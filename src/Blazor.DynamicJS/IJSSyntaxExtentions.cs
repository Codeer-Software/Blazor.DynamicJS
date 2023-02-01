namespace Blazor.DynamicJS
{
    public static class IJSSyntaxExtentions
    {
        public static dynamic New(this IJSSyntax reference, params object?[] args) => ((DynamicJS)reference).New(args);

        //and async methods
    }
}
