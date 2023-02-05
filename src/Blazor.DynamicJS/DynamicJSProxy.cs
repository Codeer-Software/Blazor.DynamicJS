using System.Reflection;

namespace Blazor.DynamicJS
{
    class DynamicJSProxy<TInterface> : DispatchProxy, IDynamicJSOwner
    {
        public DynamicJS? DynamicJS { get; private set; }

        internal static TInterface CreateEx(DynamicJS? _core)
        {
            var x = Create<TInterface, DynamicJSProxy<TInterface>>();
            ((DynamicJSProxy<TInterface>)(object)x!).DynamicJS= _core;
            return x!;
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
            => DynamicJS!.InvokeProxyMethod(targetMethod, args);
    }
}
