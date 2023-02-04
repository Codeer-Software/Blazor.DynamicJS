using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public static class IJSRuntimeExtentions
    {
        public static string HelperJSPath { get; set; } = "./blazor.dynamicjs/helper.js";

        public static async Task<DynamicJSRuntime> CreateDymaicRuntimeAsync(this IJSRuntime jsRuntime)
        {
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", HelperJSPath);
            return new DynamicJSRuntime(module);
        }
    }
}
