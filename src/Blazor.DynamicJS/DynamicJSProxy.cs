using System.Reflection;

namespace Blazor.DynamicJS
{
    public interface IDymamicJSProxy {
        TInterface Cast<TInterface>();
    }

    class DynamicJSProxy<TInterface> : DispatchProxy, IDynamicJSOwner, IDymamicJSProxy
    {
        public DynamicJS? DynamicJS { get; private set; }

        internal static TInterface CreateEx(DynamicJS? _core)
        {
            var x = Create<TInterface, DynamicJSProxy<TInterface>>();
            ((DynamicJSProxy<TInterface>)(object)x!).DynamicJS = _core;
            return x!;
        }

        internal static async Task<TInterface> CreateExAsync(DynamicJS? _core)
        {
            await Task.CompletedTask;
            return CreateEx(_core);
        }

        public TDst Cast<TDst>() => DynamicJS!.AssignInterface<TDst>();

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
            => DynamicJS!.InvokeProxyMethod(targetMethod, args);
    }
}
