using System.Reflection;

namespace Blazor.DynamicJS
{
    class DynamicJSProxy<TInterface> : DispatchProxy
    {
        DynamicJS? _core;

        internal static TInterface CreateEx(DynamicJS? _core)
        {
            var x = Create<TInterface, DynamicJSProxy<TInterface>>();
            ((DynamicJSProxy<TInterface>)(object)x!)._core= _core;
            return x!;
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
            => _core!.InvokeProxyMethod(targetMethod, args);
    }
}
