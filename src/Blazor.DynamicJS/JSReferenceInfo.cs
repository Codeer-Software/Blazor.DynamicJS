namespace Blazor.DynamicJS
{
    public class JSReferenceInfo
    {
        public long BlazorDynamicJavaScriptObjectId { get; set; }
        public List<string> BlazorDynamicJavaScriptUnresolvedNames { get; set; } = new List<string>();
    }
}
