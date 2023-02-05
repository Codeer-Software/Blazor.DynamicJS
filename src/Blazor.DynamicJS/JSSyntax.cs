namespace Blazor.DynamicJS
{
    public class JSSyntax
    {
        DynamicJS _core;
        public JSSyntax(dynamic core) => _core = core;

        public dynamic New(params object?[] args) => _core.New(args);
        public TInterface New<TInterface>(params object?[] args)=>_core.New<TInterface>(args);
        public async Task<dynamic> NewAsync(params object?[] args)=> await _core.NewAsync(args);
        public async Task<TInterface> NewAsync<TInterface>(params object?[] args) => await _core.NewAsync<TInterface>(args);
        public async Task<dynamic> InvokeAsync(params object?[] args) => await _core.InvokeAsync(args);
        public async Task SetValueAsync(object? value) => await _core.SetValueAsync(value);
        public async Task SetIndexValueAsync(object index, object? value) => await _core.SetIndexValueAsync(index, value);
        public async Task<T> GetValueAsync<T>() => await _core.GetValueAsync<T>();
        public async Task<T> GetIndexValueAsync<T>(object index) => await _core.GetIndexValueAsync<T>(index);
        public TInterface Pin<TInterface>()=> _core.Pin<TInterface>();
    }
}
