using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public static class IJSRuntimeExtentions
    {
        const string HelperJSPath = "/_content/Blazor.DynamicJS/helper.js";

        public static async Task<DynamicJSRuntime> CreateDymaicRuntimeAsync(this IJSRuntime jsRuntime)
        {
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", HelperJSPath);
            return new DynamicJSRuntime(module);
        }
    }
}
