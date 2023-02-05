using System.Reflection;

namespace Blazor.DynamicJS
{
    static class ReflectionHelper
    {
        internal static object? InvokeGenericStaticMethod(Type type, Type[] genericArguments, string method, object?[] args)
            => type.MakeGenericType(genericArguments)
                    .GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!
                    .Invoke(null, args);

        internal static object Create(Type type, params object[] args)
            => type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, args.Select(e=>e.GetType()).ToArray())!.Invoke(args)!;
    }
}
