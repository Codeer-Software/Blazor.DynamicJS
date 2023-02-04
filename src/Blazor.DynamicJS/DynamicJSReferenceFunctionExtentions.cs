using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public static class DynamicJSReferenceFunctionExtentions
    {
        //to extention methods

        public static dynamic ToJSFuction(this DynamicJSReference js, Action action)
            => js.ToJSFunctionCore(DotNetObjectReference.Create(new JSFunctionVoid(action)));

        public static dynamic ToJSFuction<T0>(this DynamicJSReference js, Action<T0> action)
            => js.ToJSFunctionCore(DotNetObjectReference.Create(new JSFunctionVoid<T0>(action)));

        public static dynamic ToJSFuction<T0, T1>(this DynamicJSReference js, Action<T0, T1> action)
            => js.ToJSFunctionCore(DotNetObjectReference.Create(new JSFunctionVoid<T0, T1>(action)));

        public static dynamic ToJSFuction<T0, T1, T2>(this DynamicJSReference js, Action<T0, T1, T2> action)
            => js.ToJSFunctionCore(DotNetObjectReference.Create(new JSFunctionVoid<T0, T1, T2>(action)));



        public static dynamic ToJSFuction<R>(this DynamicJSReference js, Func<R> func)
            => js.ToJSFunctionCore(DotNetObjectReference.Create(new JSFunction<R>(func)));

        public static dynamic ToJSFuction<T0, R>(this DynamicJSReference js, Func<T0, R> func)
            => js.ToJSFunctionCore(DotNetObjectReference.Create(new JSFunction<T0, R>(func)));

        public static dynamic ToJSFuction<T0, T1, R>(this DynamicJSReference js, Func<T0, T1, R> func)
            => js.ToJSFunctionCore(DotNetObjectReference.Create(new JSFunction<T0, T1, R>(func)));

        public static dynamic ToJSFuction<T0, T1, T2, R>(this DynamicJSReference js, Func<T0, T1, T2, R> func)
            => js.ToJSFunctionCore(DotNetObjectReference.Create(new JSFunction<T0, T1, T2, R>(func)));
    }
}
