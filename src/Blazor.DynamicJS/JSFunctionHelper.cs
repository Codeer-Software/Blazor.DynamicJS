﻿using System.Reflection;
using System.Runtime.InteropServices;

namespace Blazor.DynamicJS
{
    internal static class JSFunctionHelper
    {
        internal static bool Create(DynamicJSRuntime js, object obj, out object func, out int[] dynamicIndexes)
        {
            func = new object();
            dynamicIndexes = new int[0]; 

            var t = obj.GetType();
            if (!IsDelegate(t)) return false;

            var method = t.GetMethod("Invoke");

            var type = GetJSFunctionType(method!);
            var generics = GetGenerics(method!);
            type = generics.Any() ? type.MakeGenericType(generics) : type;
            func = type.GetConstructors().First().Invoke(new[] { js, obj });

            dynamicIndexes = method!.GetParameters().Select((e, i) => new { e.ParameterType, i }).Where(e => e.ParameterType == typeof(object)).Select(e => e.i).ToArray();
            return true;
        }

        static bool IsDelegate(Type t)
            => t.IsSubclassOf(typeof(Delegate)) || t.Equals(typeof(Delegate));

        static Type[] GetGenerics(MethodInfo m)
        {
            static Type ConvertPramTypeC2J(Type e)
                => e == typeof(object) ? typeof(long) : e;

            var csParamTypes = m.GetParameters().Select(x => x.ParameterType);
            var jsParamTypes = csParamTypes.Select(e => ConvertPramTypeC2J(e));

            //void
            if (m.ReturnParameter.ParameterType == typeof(void))
                return csParamTypes.Concat(jsParamTypes).ToArray();

            //return
            var csReturnType = m.ReturnParameter.ParameterType;
            var jsReturnType = csReturnType == typeof(object) ? typeof(JSReferenceInfo) : csReturnType;
            var list = new List<Type>();
            list.AddRange(csParamTypes);
            list.Add(csReturnType);
            list.AddRange(jsParamTypes);
            list.Add(jsReturnType);
            return list.ToArray();
        }

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
                }
            }
            throw new NotImplementedException();
        }
    }
}
