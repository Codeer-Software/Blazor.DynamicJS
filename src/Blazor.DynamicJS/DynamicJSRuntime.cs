using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public class DynamicJSRuntime : IDisposable, IAsyncDisposable
    {
        readonly Guid _guid;
        readonly IJSRuntime _jsRuntime;

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

        public dynamic GetWindow() => new DynamicJS(_jsRuntime, 0, new List<string>());


        internal dynamic ToJSFunctionCore(IDisposable objRef)
        {
            Disposables.Add(objRef);

            //TODO adjust dynamic args

            var sync = (IJSInProcessRuntime)_jsRuntime;
            var id = sync.Invoke<long>("window.BlazorDynamicJavaScriptHelper.createFunction", objRef, "Function");
            return new DynamicJS(_jsRuntime, id, new List<string>());
        }
    }
}
