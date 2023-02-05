namespace Blazor.DynamicJS
{
    public interface IJSSyntax
    {
        dynamic New(params object?[] args);
        Task<dynamic> NewAsync(params object?[] args);
        Task<dynamic> InvokeAsync(params object?[] args);
        TInterface Pin<TInterface>();
    }
}
