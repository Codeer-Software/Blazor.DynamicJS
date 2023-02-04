using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public static class IJSRuntimeExtentions
    {
        public static async Task<DynamicJSRuntime> CreateDymaicRuntimeAsync(this IJSRuntime jsRuntime)
        {
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./blazor.dynamicjs/helper.js");
            return new DynamicJSRuntime(module);
        }
    }
}
