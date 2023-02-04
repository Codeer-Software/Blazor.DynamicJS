using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public static class IJSRuntimeExtentions
    {
        public static async Task<DynamicJSReference> CreateDymaicReferenceAsync(this IJSRuntime jsRuntime)
        {
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./blazor.dynamicjs/helper.js");
            return new DynamicJSReference(module);
        }
    }
}
