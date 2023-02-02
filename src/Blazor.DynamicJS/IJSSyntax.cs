namespace Blazor.DynamicJS
{
    public interface IJSSyntax
    {
        dynamic New(params object?[] args);
    }
}
