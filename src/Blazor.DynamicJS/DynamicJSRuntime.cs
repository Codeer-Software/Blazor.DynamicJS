using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public class DynamicJSRuntime : IDisposable, IAsyncDisposable
    {
        readonly Guid _guid;
        readonly IJSInProcessObjectReference _helper;
        List<IDisposable> _disposables = new List<IDisposable>();

        internal DynamicJSRuntime(IJSInProcessObjectReference helper)
        {
            _helper = helper;
            _guid = Guid.NewGuid();
        }

        public void Dispose()
        {
            _disposables.ForEach(e => e.Dispose());
            _disposables = new List<IDisposable>();
            _helper.InvokeVoid("dispose", _guid);
        }

        public async ValueTask DisposeAsync()
        {
            _disposables.ForEach(e => e.Dispose());
            _disposables = new List<IDisposable>();
            await _helper.InvokeVoidAsync("dispose", _guid);
        }

        public dynamic GetWindow()
            => new DynamicJS(this, 0, new List<string>());

        public async Task<dynamic> ImportAsync(string path)
        {
            var objId = await _helper.InvokeAsync<long>("importModule", _guid, path);
            return new DynamicJS(this, objId, new List<string>());
        }

        public dynamic ToJS(object obj)
        {
            var function = ToJSFunction(obj);
            if (function != null) return function;

            var objId = _helper.Invoke<long>("setObject", _guid, obj);
            return new DynamicJS(this, objId, new List<string>());
        }

        public async Task<dynamic> ToJSAsync(object obj)
        {
            var function = ToJSFunction(obj);
            if (function != null) return function;

            var objId = await _helper.InvokeAsync<long>("setObject", _guid, obj);
            return new DynamicJS(this, objId, new List<string>());
        }

        internal void SetValue(long objId, List<string> accessor, object? value)
            => _helper.InvokeVoid("setProperty", objId, accessor, AdjustObject(value));

        internal async Task SetValueAsync(long objId, List<string> accessor, object? value)
            => await _helper.InvokeVoidAsync("setProperty", objId, accessor, AdjustObject(value));

        internal DynamicJS InvokeMethod(long objId, List<string> accessor, object?[] args)
        {
            var retObjId = _helper.Invoke<long>("invokeMethod", _guid, objId, accessor, AdjustArguments(args!));
            return new DynamicJS(this, retObjId, new List<string>());
        }

        internal async Task<DynamicJS> InvokeAsync(long objId, List<string> accessor, object?[] args)
        {
            var retObjId = await _helper.InvokeAsync<long>("invokeMethod", _guid, objId, accessor, AdjustArguments(args!));
            return new DynamicJS(this, retObjId, new List<string>());
        }

        internal object? Convert(Type type, long objId, List<string> accessor)
            => ReflectionHelper.InvokeGenericStaticMethod(
                typeof(Converter<>), new[] { type },
                "Convert", new object[] { _helper, objId, accessor });

        internal async Task<object?> ConvertAsync(Type type, long objId, List<string> accessor)
            => await ((Task<object?>)ReflectionHelper.InvokeGenericStaticMethod(
                typeof(Converter<>), new[] { type },
                "ConvertAsync", new object[] { _helper, objId, accessor })!);

        internal DynamicJS InvokeFunctionObject(long objId, List<string> accessor, object?[] args)
        {
            var retObjId = _helper.Invoke<long>("invokeMethod", _guid, objId, accessor, AdjustArguments(args!));
            return new DynamicJS(this, retObjId, new List<string>());
        }

        internal DynamicJS GetIndex(long objId, List<string> accessor, object[] indexes)
        {
            var retObjId = _helper.Invoke<long>("getIndex", _guid, objId, accessor, indexes[0]);
            return new DynamicJS(this, retObjId, new List<string>());
        }

        internal async Task<DynamicJS> GetIndexAsync(long objId, List<string> accessor, object[] indexes)
        {
            var retObjId = await _helper.InvokeAsync<long>("getIndex", _guid, objId, accessor, indexes[0]);
            return new DynamicJS(this, retObjId, new List<string>());
        }

        internal void SetIndex(long objId, List<string> accessor, object[] indexes, object? value)
        {
            _helper.InvokeVoid("setIndex", objId, accessor, indexes[0], value);
        }

        internal async Task SetIndexAsync(long objId, List<string> accessor, object[] indexes, object? value)
        {
            await _helper.InvokeVoidAsync("setIndex", objId, accessor, indexes[0], value);
        }

        internal DynamicJS New(List<string> accessor, object?[] args)
        {
            var objId = _helper.Invoke<long>("createObject", _guid, accessor, AdjustArguments(args!));
            return new DynamicJS(this, objId, new List<string>());
        }

        internal async Task<DynamicJS> NewAsync(List<string> accessor, object?[] args)
        {
            var objId = await _helper.InvokeAsync<long>("createObject", _guid, accessor, AdjustArguments(args!));
            return new DynamicJS(this, objId, new List<string>());
        }

        internal C J2C<J, C>(J src)
        {
            if (typeof(C) == typeof(object))
            {
                return (C)(object)new DynamicJS(this, (long)(object)src!, new List<string>());
            }
            return (C)(object)src!;
        }

        internal J C2J<C, J>(C src)
        {
            if (src is DynamicJS ds)
            {
                var x = typeof(J).ToString();
                return (J)(object)ds.ToJsonable();
            }
            return (J)(object)src!;
        }

        object?[] AdjustArguments(object[] args)
            => args.Select(e => AdjustObject(e)).ToArray();

        object? AdjustObject(object? src)
        {
            if (src is DynamicJS dynamicJs) return dynamicJs.ToJsonable();

            var function = ToJSFunction(src);
            if (function != null) return ((DynamicJS)function).ToJsonable();

            return src;
        }

        dynamic? ToJSFunction(object? obj)
        {
            if (obj == null) return null;

            if (!JSFunctionHelper.Create(this, obj, out var function, out var dynamicIndexes)) return null;

            var objRef = (IDisposable)ReflectionHelper.InvokeGenericStaticMethod(
                typeof(DotNetObjectReferenceWrapper<>), new[] { function.GetType() },
                "Create", new object[] { function })!;

            _disposables.Add(objRef);
            var objId = _helper.Invoke<long>("createFunction", _guid, objRef, "Function", dynamicIndexes);
            return new DynamicJS(this, objId, new List<string>());
        }

        class DotNetObjectReferenceWrapper<T> where T : class
        {
            internal static DotNetObjectReference<T> Create(T obj)
                => DotNetObjectReference.Create(obj);
        }

        class Converter<T>
        {
            internal static T Convert(IJSInProcessObjectReference inProcess, long objId, List<string> accessor)
                => inProcess.Invoke<T>("getObject", objId, accessor);

            internal static async Task<object?> ConvertAsync(IJSInProcessObjectReference inProcess, long objId, List<string> accessor)
                => await inProcess.InvokeAsync<T>("getObject", objId, accessor);
        }
    }
}
