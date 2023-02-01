using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public static class IJSRuntimeExtentions
    {
        public static DynamicJSRuntime CreateDymaicRuntime(this IJSRuntime jsRuntime) => new DynamicJSRuntime(jsRuntime);
    }
}
