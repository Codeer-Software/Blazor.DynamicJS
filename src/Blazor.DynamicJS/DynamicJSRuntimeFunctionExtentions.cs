using Microsoft.JSInterop;
using System;
using static Blazor.DynamicJS.DynamicJSRuntime;

namespace Blazor.DynamicJS
{
    public static class DynamicJSRuntimeFunctionExtentions
    {
        //to extention methods

        public static dynamic ToJSFuction(this DynamicJSRuntime js, Action action)
            => js.ToJSFunctionCore(typeof(JSFunctionVoid), new Type[0], action);

        public static dynamic ToJSFuction<T0>(this DynamicJSRuntime js, Action<T0> action)
            => js.ToJSFunctionCore(typeof(JSFunctionVoid<>), new[] { typeof(T0) }, action);

        public static dynamic ToJSFuction<T0, T1>(this DynamicJSRuntime js, Action<T0, T1> action)
            => js.ToJSFunctionCore(typeof(JSFunctionVoid<,>), new[] { typeof(T0), typeof(T1) }, action);

        public static dynamic ToJSFuction<T0, T1, T2>(this DynamicJSRuntime js, Action<T0, T1, T2> action)
            => js.ToJSFunctionCore(typeof(JSFunctionVoid<,,>), new[] { typeof(T0), typeof(T1), typeof(T2) }, action);


        public static dynamic ToJSFuction<R>(this DynamicJSRuntime js, Func<R> func)
           => js.ToJSFunctionCore(typeof(JSFunction<>), new[] { typeof(R) }, func);

        public static dynamic ToJSFuction<T0, R>(this DynamicJSRuntime js, Func<T0, R> func)
           => js.ToJSFunctionCore(typeof(JSFunction<,>), new[] { typeof(T0), typeof(R) }, func);

        public static dynamic ToJSFuction<T0, T1, R>(this DynamicJSRuntime js, Func<T0, T1, R> func)
           => js.ToJSFunctionCore(typeof(JSFunction<,,>), new[] { typeof(T0), typeof(T1), typeof(R) }, func);

        public static dynamic ToJSFuction<T0, T1, T2, R>(this DynamicJSRuntime js, Func<T0, T1, T2, R> func)
           => js.ToJSFunctionCore(typeof(JSFunction<,,,>), new[] { typeof(T0), typeof(T1), typeof(T2), typeof(R) }, func);





    }
}
