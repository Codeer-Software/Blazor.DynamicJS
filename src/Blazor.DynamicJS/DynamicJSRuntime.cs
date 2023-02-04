using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public class DynamicJSRuntime : IDisposable, IAsyncDisposable
    {
        readonly Guid _guid;
        readonly IJSObjectReference _helper;
        List<IDisposable> _disposables = new List<IDisposable>();

        IJSInProcessObjectReference InProcessHelper => (IJSInProcessObjectReference)_helper;

        internal DynamicJSRuntime(IJSObjectReference helper)
        {
            _helper = helper;
            _guid = Guid.NewGuid();
        }

        public void Dispose()
        {
            _disposables.ForEach(e => e.Dispose());
            _disposables = new List<IDisposable>();
            InProcessHelper.InvokeVoid("dispose", _guid);
        }

        public async ValueTask DisposeAsync()
        {
            _disposables.ForEach(e => e.Dispose());
            _disposables = new List<IDisposable>();
            await _helper.InvokeVoidAsync("dispose", _guid);
        }

        public dynamic GetWindow() => new DynamicJS(this, 0, new List<string>());

        public async Task<dynamic> ImportAsync(string path)
        {
            var id = await _helper.InvokeAsync<long>("importModule", _guid, path);
            return new DynamicJS(this, id, new List<string>());
        }

        public dynamic ToJS(object obj)
        {
            if (JSFunctionHelper.Create(this, obj, out var function, out var dynamicIndexes))
            {
                var wrapper = typeof(DotNetObjectReferenceWrapper<>).MakeGenericType(function.GetType());
                var objRef = (IDisposable)wrapper.GetMethod("Create")!.Invoke(null, new object[] { function })!;

                _disposables.Add(objRef);
                var id = InProcessHelper.Invoke<long>("createFunction", _guid, objRef, "Function", dynamicIndexes);
                return new DynamicJS(this, id, new List<string>());
            }
            else
            {
                var id = InProcessHelper.Invoke<long>("setObject", _guid, obj);
                return new DynamicJS(this, id, new List<string>());
            }
        }

        public class DotNetObjectReferenceWrapper<T> where T : class
        {
            public static DotNetObjectReference<T> Create(T obj) => DotNetObjectReference.Create(obj);
        }

        internal void SetValue(long id, List<string> accessor, object? value)
        {
            InProcessHelper.InvokeVoid("setProperty", id, accessor, AdjustObject(value));
        }

        internal DynamicJS InvokeMethod(long id, List<string> accessor, object?[] args)
        {
            var ret = InProcessHelper.Invoke<long>("invokeMethod", _guid, id, accessor, AdjustArguments(args!));
            return new DynamicJS(this, ret, new List<string>());
        }

        internal async Task<dynamic> InvokeAsync(long id, List<string> accessor, object?[] args)
        {
            var ret = await _helper.InvokeAsync<long>("invokeMethod", _guid, id, accessor, AdjustArguments(args!));
            return new DynamicJS(this, ret, new List<string>());
        }

        internal object? Convert(Type type, long id, List<string> accessor)
        {
            var converter = typeof(Converter<>).MakeGenericType(type);
            return converter.GetMethod("Convert")!.Invoke(null, new object[] { _helper, id, accessor });
        }
        
        internal DynamicJS InvokeFunctionObject(long id, List<string> accessor, object?[] args)
        {
            var ret = InProcessHelper.Invoke<long>("invokeMethod", _guid, id, accessor, AdjustArguments(args!));
            return new DynamicJS(this, ret, new List<string>());
        }

        internal object? GetIndex(long id, List<string> accessor, object[] indexes)
        {
            var ret = InProcessHelper.Invoke<long>("getIndex", _guid, id, accessor, indexes[0]);
            return new DynamicJS(this, ret, new List<string>());
        }

        internal void SetIndex(long id, List<string> accessor, object[] indexes, object? value)
        {
            InProcessHelper.InvokeVoid("setIndex", id, accessor, indexes[0], value);
        }

        public class Converter<T>
        {
            public static T Convert(IJSInProcessObjectReference inProcess, long id, List<string> accessor)
            {
                return inProcess.Invoke<T>("getObject", id, accessor);
            }
        }

        internal DynamicJS New(List<string> accessor, object?[] args)
        {
            var id = InProcessHelper.Invoke<long>("createObject", _guid, accessor, AdjustArguments(args!));
            return new DynamicJS(this, id, new List<string>());
        }

        internal async Task<dynamic> NewAsync(List<string> accessor, object?[] args)
        {
            var id = await _helper.InvokeAsync<long>("createObject", _guid, accessor, AdjustArguments(args!));
            return new DynamicJS(this, id, new List<string>());
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
                return (J)(object)ds.Marshal();
            }
            return (J)(object)src!;
        }

        static object[] AdjustArguments(object[] args)
            => args.Select(e => AdjustObject(e)).ToArray();

        static object AdjustObject(object? e)
        {
            if (e is DynamicJS r) return r.Marshal();
            return e;
        }
    }
}
