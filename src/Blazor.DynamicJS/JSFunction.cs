using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    //TODO resolve args and return value


    public class JSFunctionVoid
    {
        Action _core;

        public JSFunctionVoid(Action core) => _core = core;

        [JSInvokable]
        public void Function() => _core();
    }

    public class JSFunctionVoid<T0>
    {
        Action<T0> _core;

        public JSFunctionVoid(Action<T0> core) => _core = core;

        [JSInvokable]
        public void Function(T0 t0) => _core(t0);
    }

    public class JSFunctionVoid<T0, T1>
    {
        Action<T0, T1> _core;

        public JSFunctionVoid(Action<T0, T1> core) => _core = core;

        [JSInvokable]
        public void Function(T0 t0, T1 t1) => _core(t0, t1);
    }

    public class JSFunctionVoid<T0, T1, T2>
    {
        Action<T0, T1, T2> _core;

        public JSFunctionVoid(Action<T0, T1, T2> core) => _core = core;

        [JSInvokable]
        public void Function(T0 t0, T1 t1, T2 t2) => _core(t0, t1, t2);
    }

    //-------

    public class JSFunction<R>
    {
        Func<R> _core;

        public JSFunction(Func<R> core) => _core = core;

        [JSInvokable]
        public R Function() => _core();
    }

    public class JSFunction<T0, R>
    {
        Func<T0, R> _core;

        public JSFunction(Func<T0, R> core) => _core = core;

        [JSInvokable]
        public R Function(T0 t0) => _core(t0);
    }

    public class JSFunction<T0, T1, R>
    {
        Func<T0, T1, R> _core;

        public JSFunction(Func<T0, T1, R> core) => _core = core;

        [JSInvokable]
        public R Function(T0 t0, T1 t1) => _core(t0, t1);
    }

    public class JSFunction<T0, T1, T2, R>
    {
        Func<T0, T1, T2, R> _core;

        public JSFunction(Func<T0, T1, T2, R> core) => _core = core;

        [JSInvokable]
        public R Function(T0 t0, T1 t1, T2 t2) => _core(t0, t1, t2);
    }
}
