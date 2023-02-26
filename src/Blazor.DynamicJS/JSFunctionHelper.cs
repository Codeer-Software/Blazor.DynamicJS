using System.Reflection;

namespace Blazor.DynamicJS
{
    internal static class JSFunctionHelper
    {
        internal static bool Create(DynamicJSRuntime js, object obj, out object func, out int argsCount, out int[] dynamicIndexes, out bool isAsync)
        {
            isAsync = false;
            func = new object();
            dynamicIndexes = new int[0];
            argsCount = 0;

            var t = obj.GetType();
            if (!IsDelegate(t)) return false;

            var method = t.GetMethod("Invoke");

            isAsync = IsTask(method!.ReturnType);

            var type = GetJSFunctionType(method!);
            var generics = GetGenerics(method!);

            type = generics.Any() ? type.MakeGenericType(generics) : type;
            func = ReflectionHelper.Create(type, js, obj);

            argsCount = method!.GetParameters().Length;
            dynamicIndexes = method!.GetParameters().Select((e, i) => new { e.ParameterType, i }).Where(e => IsReferenceType(e.ParameterType)).Select(e => e.i).ToArray();
            return true;
        }

        static bool IsTask(Type returnType)
            => returnType == typeof(Task) || (IsGenericTask(returnType));

        static bool IsGenericTask(Type returnType)
            => returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>);

        static bool IsDelegate(Type t)
            => t.IsSubclassOf(typeof(Delegate)) || t.Equals(typeof(Delegate));

        static Type[] GetGenerics(MethodInfo m)
        {
            static Type ConvertPramTypeC2J(Type e)
                => IsReferenceType(e) ? typeof(long) : e;

            var csParamTypes = m.GetParameters().Select(x => x.ParameterType);
            var jsParamTypes = csParamTypes.Select(e => ConvertPramTypeC2J(e));

            var csReturnType = m.ReturnParameter.ParameterType;

            //void
            if (csReturnType == typeof(void))
                return csParamTypes.Concat(jsParamTypes).ToArray();

            Type jsReturnType;
            if (IsGenericTask(csReturnType))
            {
                jsReturnType = IsReferenceType(csReturnType.GetGenericArguments()[0]) ? typeof(Task<DynamicJSJsonableData>) : csReturnType;
            }
            else
            {
                jsReturnType = IsReferenceType(csReturnType) ? typeof(DynamicJSJsonableData) : csReturnType;
            }

            //return
            var list = new List<Type>();
            list.AddRange(csParamTypes);
            list.Add(csReturnType);
            list.AddRange(jsParamTypes);
            list.Add(jsReturnType);
            return list.ToArray();
        }

        internal static bool IsReferenceType(Type type) => type == typeof(object) || type.IsInterface;

        static Type GetJSFunctionType(MethodInfo m)
        {
            var ps = m.GetParameters();
            var r = m.ReturnParameter;

            if (r.ParameterType == typeof(void))
            {
                switch (ps.Length)
                {
                    case 0: return typeof(JSFunctionVoid);
                    case 1: return typeof(JSFunctionVoid<,>);
                    case 2: return typeof(JSFunctionVoid<,,,>);
                    case 3: return typeof(JSFunctionVoid<,,,,,>);
                    case 4: return typeof(JSFunctionVoid<,,,,,,,>);
                    case 5: return typeof(JSFunctionVoid<,,,,,,,,,>);
                    case 6: return typeof(JSFunctionVoid<,,,,,,,,,,,>);
                    case 7: return typeof(JSFunctionVoid<,,,,,,,,,,,,,>);
                    case 8: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,>);
                    case 9: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,,,>);
                    case 10: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,,,,,>);
                    case 11: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,,,,,,,>);
                    case 12: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 13: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 14: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 15: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 16: return typeof(JSFunctionVoid<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
                }
            }
            else
            {
                switch (ps.Length)
                {
                    case 0: return typeof(JSFunction<,>);
                    case 1: return typeof(JSFunction<,,,>);
                    case 2: return typeof(JSFunction<,,,,,>);
                    case 3: return typeof(JSFunction<,,,,,,,>);
                    case 4: return typeof(JSFunction<,,,,,,,,,>);
                    case 5: return typeof(JSFunction<,,,,,,,,,,,>);
                    case 6: return typeof(JSFunction<,,,,,,,,,,,,,>);
                    case 7: return typeof(JSFunction<,,,,,,,,,,,,,,,>);
                    case 8: return typeof(JSFunction<,,,,,,,,,,,,,,,,,>);
                    case 9: return typeof(JSFunction<,,,,,,,,,,,,,,,,,,,>);
                    case 10: return typeof(JSFunction<,,,,,,,,,,,,,,,,,,,,,>);
                    case 11: return typeof(JSFunction<,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 12: return typeof(JSFunction<,,,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 13: return typeof(JSFunction<,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 14: return typeof(JSFunction<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 15: return typeof(JSFunction<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
                    case 16: return typeof(JSFunction<,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,>);
                }
            }
            throw new NotImplementedException();
        }
    }
}
