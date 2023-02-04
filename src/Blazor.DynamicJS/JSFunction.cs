using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    public class JSFunctionVoid
    {
        DynamicJSRuntime _js;
        Action _core;

        public JSFunctionVoid(DynamicJSRuntime js, Action core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function() => _core();
    }

    public class JSFunctionVoid<C0, J0>
    {
        Action<C0> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 t0) => _core(_js.J2C<J0, C0>(t0));
    }

    public class JSFunctionVoid<C0, C1, J0, J1>
    {
        Action<C0, C1> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 t0, J1 t1) => _core(_js.J2C<J0, C0>(t0), _js.J2C<J1, C1>(t1));
    }

    public class JSFunctionVoid<C0, C1, C2, J0, J1, J2>
    {
        Action<C0, C1, C2> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 t0, J1 t1, J2 t2) => _core(_js.J2C<J0, C0>(t0), _js.J2C<J1, C1>(t1), _js.J2C<J2, C2>(t2));
    }

    //-------

    public class JSFunction<RC, RJ>
    {
        Func<RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function() => _js.C2J<RC, RJ>(_core());
    }

    public class JSFunction<C0, RC, J0, RJ>
    {
        Func<C0, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 t0) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(t0)));
    }

    public class JSFunction<C0, C1, RC, J0, J1, RJ>
    {
        Func<C0, C1, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 t0, J1 t1) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(t0), _js.J2C<J1, C1>(t1)));
    }

    public class JSFunction<C0, C1, C2, RC, J0, J1, J2, RJ>
    {
        Func<C0, C1, C2, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 t0, J1 t1, J2 t2) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(t0), _js.J2C<J1, C1>(t1), _js.J2C<J2, C2>(t2)));
    }
}
