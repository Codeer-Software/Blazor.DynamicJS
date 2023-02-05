namespace Blazor.DynamicJS
{
    public interface IJSSyntax
    {
        dynamic New(params object?[] args);
        Task<dynamic> NewAsync(params object?[] args);
        Task<dynamic> InvokeAsync(params object?[] args);
        Task SetValueAsync(object? value);
        Task SetIndexValueAsync(object idnex, object? value);
        Task<T> GetValueAsync<T>();
        Task<T> GetIndexValueAsync<T>(object idnex);
        TInterface Pin<TInterface>();
    }
}
