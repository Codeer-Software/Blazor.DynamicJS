using Microsoft.JSInterop;
using System.Security.Cryptography;

namespace Blazor.DynamicJS
{
    public class DynamicJSRuntime : IDisposable, IAsyncDisposable
    {
        readonly Guid _guid;
        IJSObjectReference HelperAsync { get; }
        IJSInProcessObjectReference HelperInprocess => (IJSInProcessObjectReference)HelperAsync;

        List<IDisposable> _disposables = new List<IDisposable>();

        internal DynamicJSRuntime(IJSObjectReference helper)
        {
            //IJSInProcessObjectReference
            HelperAsync = helper;
            _guid = Guid.NewGuid();
        }

        public void Dispose()
        {
            _disposables.ForEach(e => e.Dispose());
            _disposables = new List<IDisposable>();
            HelperInprocess.InvokeVoid("dispose", _guid);
        }

        public async ValueTask DisposeAsync()
        {
            _disposables.ForEach(e => e.Dispose());
            _disposables = new List<IDisposable>();
            await HelperAsync.InvokeVoidAsync("dispose", _guid);
        }

        public dynamic GetWindow()
            => new DynamicJS(this, 0, new List<string>());

        public T GetWindow<T>()
            => new DynamicJS(this, 0, new List<string>()).AssignInterface<T>();

        public dynamic New(string fullName, params object?[] args)
            => New(0, fullName.Split('.').ToList(), args);

        public dynamic NewAsync(string fullName, params object?[] args)
            => NewAsync(0, fullName.Split('.').ToList(), args);

        public async Task<dynamic> ImportAsync(string path)
        {
            if (path.StartsWith(".")) throw new ArgumentException("Please specify with absolute path");
            var objId = await HelperAsync.InvokeAsync<long>("importModule", _guid, path);
            return new DynamicJS(this, objId, new List<string>());
        }

        public async Task<TInterface> ImportAsync<TInterface>(string path)
        {
            if (path.StartsWith(".")) throw new ArgumentException("Please specify with absolute path");
            DynamicJS mod = await ImportAsync(path);
            return mod.AssignInterface<TInterface>();
        }

        public dynamic ToJS(object obj)
        {
            var function = ToJSFunction(obj);
            if (function != null) return function;

            var objId = HelperInprocess.Invoke<long>("setObject", _guid, obj);
            return new DynamicJS(this, objId, new List<string>());
        }

        public TInterface ToJS<TInterface>(object obj)
        {
            DynamicJS js = ToJS(obj);
            return js.AssignInterface<TInterface>();
        }

        public async Task<dynamic> ToJSAsync(object obj)
        {
            var function = ToJSFunction(obj);
            if (function != null) return function;

            var objId = await HelperAsync.InvokeAsync<long>("setObject", _guid, obj);
            return new DynamicJS(this, objId, new List<string>());
        }

        public async Task<TInterface> ToJSAsync<TInterface>(object obj)
        {
            DynamicJS js = await ToJSAsync(obj);
            return js.AssignInterface<TInterface>();
        }

        internal void SetValue(long objId, List<string> accessor, object? value)
        {
            try
            {
                HelperInprocess.InvokeVoid("setProperty", objId, accessor, AdjustObject(value));
            }
            catch
            {
                throw new DynamicJSPropertyException(objId, accessor, value);
            }
        }

        internal async Task SetValueAsync(long objId, List<string> accessor, object? value)
        {
            try
            {
                await HelperAsync.InvokeVoidAsync("setProperty", objId, accessor, await AdjustObjectAsync(value));
            }
            catch
            {
                throw new DynamicJSPropertyException(objId, accessor, value);
            }
        }

        internal object? Convert(Type type, long objId, List<string> accessor)
        {
            try
            {
                return ReflectionHelper.InvokeGenericStaticMethod(
                typeof(Converter<>), new[] { type },
                "Convert", new object[] { HelperAsync, objId, accessor });
            }
            catch
            {
                throw new DynamicJSPropertyException(type, objId, accessor);
            }
        }

        internal async Task<object?> ConvertAsync(Type type, long objId, List<string> accessor)
        {
            try
            {
                return await ((Task<object?>)ReflectionHelper.InvokeGenericStaticMethod(
                    typeof(Converter<>), new[] { type },
                    "ConvertAsync", new object[] { HelperAsync, objId, accessor })!);
            }
            catch
            {
                throw new DynamicJSPropertyException(type, objId, accessor);
            }
        }

        internal DynamicJS InvokeMethod(long objId, List<string> accessor, object?[] args)
        {
            try
            {
                var retObjId = HelperInprocess.Invoke<long>("invokeMethod", _guid, objId, accessor, AdjustArguments(args!));
                return new DynamicJS(this, retObjId, new List<string>());
            }
            catch
            {
                throw new DynamicJSFunctionException(objId, accessor, args!);
            }
        }

        internal async Task<DynamicJS> InvokeAsync(long objId, List<string> accessor, object?[] args)
        {
            try
            {
                var retObjId = await HelperAsync.InvokeAsync<long>("invokeMethod", _guid, objId, accessor, await AdjustArgumentsAsync(args!));
                return new DynamicJS(this, retObjId, new List<string>());
            }
            catch
            {
                throw new DynamicJSFunctionException(objId, accessor, args!);
            }
        }

        internal async Task<T> InvokeAsync<T>(long objId, List<string> accessor, object?[] args)
        {
            try
            {
                if (typeof(T) == typeof(object))
                {
                    var retObjId = await HelperAsync.InvokeAsync<long>("invokeMethod", _guid, objId, accessor, await AdjustArgumentsAsync(args!));
                    return (dynamic) new DynamicJS(this, retObjId, new List<string>());
                }
                return await HelperAsync.InvokeAsync<T>("invokeMethodAndGetObject", _guid, objId, accessor, await AdjustArgumentsAsync(args!));
            }
            catch
            {
                throw new DynamicJSFunctionException(objId, accessor, args!);
            }
        }

        internal DynamicJS InvokeFunctionObject(long objId, List<string> accessor, object?[] args)
        {
            try
            {
                //todo inprocess only
                var retObjId = HelperInprocess.Invoke<long>("invokeMethod", _guid, objId, accessor, AdjustArguments(args!));
                return new DynamicJS(this, retObjId, new List<string>());
            }
            catch
            {
                throw new DynamicJSFunctionException(objId, accessor, args!);
            }
        }

        internal DynamicJS GetIndex(long objId, List<string> accessor, object[] indexes)
        {
            try
            {
                var retObjId = HelperInprocess.Invoke<long>("getIndex", _guid, objId, accessor, indexes[0]);
                return new DynamicJS(this, retObjId, new List<string>());
            }
            catch
            {
                throw new DynamicJSIndexPropertyException(objId, accessor, indexes);
            }
        }

        internal async Task<DynamicJS> GetIndexAsync(long objId, List<string> accessor, object[] indexes)
        {
            try
            {
                var retObjId = await HelperAsync.InvokeAsync<long>("getIndex", _guid, objId, accessor, indexes[0]);
                return new DynamicJS(this, retObjId, new List<string>());
            }
            catch
            {
                throw new DynamicJSIndexPropertyException(objId, accessor, indexes);
            }
        }

        internal void SetIndex(long objId, List<string> accessor, object[] indexes, object? value)
        {
            try
            {
                HelperInprocess.InvokeVoid("setIndex", objId, accessor, indexes[0], value);
            }
            catch
            {
                throw new DynamicJSIndexPropertyException(objId, accessor, indexes, value);
            }
        }

        internal async Task SetIndexAsync(long objId, List<string> accessor, object[] indexes, object? value)
        {
            try
            {
                await HelperAsync.InvokeVoidAsync("setIndex", objId, accessor, indexes[0], value);
            }
            catch
            {
                throw new DynamicJSIndexPropertyException(objId, accessor, indexes, value);
            }
        }

        internal DynamicJS New(long objId, List<string> accessor, object?[] args)
        {
            try
            {
                var retObjId = HelperInprocess.Invoke<long>("createObject", _guid, objId, accessor, AdjustArguments(args!));
                return new DynamicJS(this, retObjId, new List<string>());
            }
            catch
            {
                throw new DynamicJSNewException(objId, accessor, args!);
            }
        }

        internal async Task<DynamicJS> NewAsync(long objId, List<string> accessor, object?[] args)
        {
            try
            {
                var retObjId = await HelperAsync.InvokeAsync<long>("createObject", _guid, objId, accessor, await AdjustArgumentsAsync(args!));
                return new DynamicJS(this, retObjId, new List<string>());
            }
            catch
            {
                throw new DynamicJSNewException(objId, accessor, args!);
            }
        }

        internal C J2C<J, C>(J src)
        {
            if (typeof(C) == typeof(object))
            {
                return (C)(object)new DynamicJS(this, (long)(object)src!, new List<string>());
            }
            else if (typeof(C).IsInterface)
            {
                return new DynamicJS(this, (long)(object)src!, new List<string>()).AssignInterface<C>();
            }
            return (C)(object)src!;
        }

        internal J C2J<C, J>(C src)
        {
            if (src is DynamicJS ds)
            {
                return (J)(object)ds.ToJsonable();
            }
            if (src is IDynamicJSOwner owner)
            {
                return (J)(object)owner.DynamicJS!.ToJsonable();
            }
            return (J)(object)src!;
        }

        object?[] AdjustArguments(object[] args)
            => args.Select(e => AdjustObject(e)).ToArray();

        //todo performance
        async Task<object?[]> AdjustArgumentsAsync(object[] args)
        {
            var list = new List<object?>();
            foreach (var e in args)
            { 
                list.Add(await AdjustObjectAsync(e));
            }
            return list.ToArray();
        }

        object? AdjustObject(object? src)
        {
            if (src is IDynamicJSOwner dynamicJSOwner) return dynamicJSOwner.DynamicJS!.ToJsonable();

            if (src is DynamicJS dynamicJs) return dynamicJs.ToJsonable();

            var function = ToJSFunction(src);
            if (function != null) return ((DynamicJS)function).ToJsonable();

            return src;
        }

        async Task<object?> AdjustObjectAsync(object? src)
        {
            //todo refactoring
            if (src is IDynamicJSOwner dynamicJSOwner) return dynamicJSOwner.DynamicJS!.ToJsonable();

            if (src is DynamicJS dynamicJs) return dynamicJs.ToJsonable();

            var function = await ToJSFunctionAsync(src);
            if (function != null) return ((DynamicJS)function).ToJsonable();

            return src;
        }

        dynamic? ToJSFunction(object? obj)
        {
            if (obj == null) return null;

            if (!JSFunctionHelper.Create(this, obj, out var function, out var argsCount, out var dynamicIndexes, out var isAsync)) return null;

            var objRef = (IDisposable)ReflectionHelper.InvokeGenericStaticMethod(
                typeof(DotNetObjectReferenceWrapper<>), new[] { function.GetType() },
                "Create", new object[] { function })!;

            _disposables.Add(objRef);
            var objId = isAsync ?
                HelperInprocess.Invoke<long>("createAsyncFunction", _guid, objRef, "Function", argsCount, dynamicIndexes) :
                HelperInprocess.Invoke<long>("createFunction", _guid, objRef, "Function", argsCount, dynamicIndexes);
            return new DynamicJS(this, objId, new List<string>());
        }

        async Task<dynamic?> ToJSFunctionAsync(object? obj)
        {
            //todo refactoring
            if (obj == null) return null;

            if (!JSFunctionHelper.Create(this, obj, out var function, out var argsCount, out var dynamicIndexes, out var isAsync)) return null;

            var objRef = (IDisposable)ReflectionHelper.InvokeGenericStaticMethod(
                typeof(DotNetObjectReferenceWrapper<>), new[] { function.GetType() },
                "Create", new object[] { function })!;

            _disposables.Add(objRef);
            var objId = isAsync ?
                HelperInprocess.InvokeAsync<long>("createAsyncFunction", _guid, objRef, "Function", argsCount, dynamicIndexes) :
                HelperInprocess.InvokeAsync<long>("createFunction", _guid, objRef, "Function", argsCount,  dynamicIndexes);
            return new DynamicJS(this, await objId, new List<string>());
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
