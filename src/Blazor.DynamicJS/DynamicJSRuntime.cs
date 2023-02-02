using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public class DynamicJSRuntime : IDisposable, IAsyncDisposable
    {
        readonly Guid _guid;
        readonly IJSRuntime _jsRuntime;

        IJSInProcessRuntime InProcess => (IJSInProcessRuntime)_jsRuntime;

        internal DynamicJSRuntime(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _guid = Guid.NewGuid();
        }
        internal List<IDisposable> Disposables { get; } = new List<IDisposable>();

        public void Dispose()
        {
        }

        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public dynamic GetWindow() => new DynamicJS(this, 0, new List<string>());

        //return IJSFunction or JSSyntax
        internal dynamic ToJSFunctionCore(IDisposable objRef)
        {
            Disposables.Add(objRef);

            //TODO adjust dynamic args

            var id = InProcess.Invoke<long>("window.BlazorDynamicJavaScriptHelper.createFunction", objRef, "Function");
            return new DynamicJS(this, id, new List<string>());
        }


        internal void SetValue(long id, List<string> accessor, object? value)
        {
            InProcess.InvokeVoid("window.BlazorDynamicJavaScriptHelper.setProperty", id, accessor, value);
        }

        internal DynamicJS InvokeMethod(long id, List<string> accessor, object?[] args)
        {
            //adjust funcobjects
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJS r) args[i] = r.Marshal();
            }

            var ret = InProcess.Invoke<long>("window.BlazorDynamicJavaScriptHelper.invokeMethod", id, accessor, args);
            return new DynamicJS(this, ret, new List<string>());
        }

        internal object? Convert(Type type, long id, List<string> accessor)
        {
            var converter = typeof(Converter<>).MakeGenericType(type);
            return converter.GetMethod("Convert")!.Invoke(null, new object[] { _jsRuntime, id, accessor });
        }
        
        internal DynamicJS InvokeFunctionObject(long id, List<string> accessor, object?[] args)
        {
            //adjust funcobjects
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJS r) args[i] = r.Marshal();
            }

            var ret = InProcess.Invoke<long>("window.BlazorDynamicJavaScriptHelper.invokeMethod", id, accessor, args);
            return new DynamicJS(this, ret, new List<string>());
        }

        internal object? GetIndex(long id, List<string> accessor, object[] indexes)
        {
            var ret = InProcess.Invoke<long>("window.BlazorDynamicJavaScriptHelper.getIndex", id, accessor, indexes[0]);
            return new DynamicJS(this, ret, new List<string>());
        }

        internal void SetIndex(long id, List<string> accessor, object[] indexes, object? value)
        {
            InProcess.InvokeVoid("window.BlazorDynamicJavaScriptHelper.setIndex", id, accessor, indexes[0], value);
        }

        public class Converter<T>
        {
            public static T Convert(IJSInProcessRuntime inProcess, long id, List<string> accessor)
            {
                return inProcess.Invoke<T>("window.BlazorDynamicJavaScriptHelper.getObject", id, accessor);
            }
        }

        internal DynamicJS New(List<string> accessor, object?[] args)
        {
            //adjust funcobjects
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJS r) args[i] = r.Marshal();
            }

            var id = InProcess.Invoke<long>("window.BlazorDynamicJavaScriptHelper.createObject", accessor, args);
            return new DynamicJS(this, id, new List<string>());
        }
    }
}
