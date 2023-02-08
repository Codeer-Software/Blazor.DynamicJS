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
        public void Function(J0 a0) => _core(_js.J2C<J0, C0>(a0));
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
        public void Function(J0 a0, J1 a1) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1));
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
        public void Function(J0 a0, J1 a1, J2 a2) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, J0, J1, J2, J3>
    {
        Action<C0, C1, C2, C3> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, J0, J1, J2, J3, J4>
    {
        Action<C0, C1, C2, C3, C4> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, J0, J1, J2, J3, J4, J5>
    {
        Action<C0, C1, C2, C3, C4, C5> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, J0, J1, J2, J3, J4, J5, J6>
    {
        Action<C0, C1, C2, C3, C4, C5, C6> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, J0, J1, J2, J3, J4, J5, J6, J7>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, C8, J0, J1, J2, J3, J4, J5, J6, J7, J8>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7, C8> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7, C8> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11, J12 a12) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11), _js.J2C<J12, C12>(a12));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, J13>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11, J12 a12, J13 a13) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11), _js.J2C<J12, C12>(a12), _js.J2C<J13, C13>(a13));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, J13, J14>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11, J12 a12, J13 a13, J14 a14) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11), _js.J2C<J12, C12>(a12), _js.J2C<J13, C13>(a13), _js.J2C<J14, C14>(a14));
    }

    public class JSFunctionVoid<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, C15, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, J13, J14, J15>
    {
        Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, C15> _core;
        DynamicJSRuntime _js;

        public JSFunctionVoid(DynamicJSRuntime js, Action<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, C15> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public void Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11, J12 a12, J13 a13, J14 a14, J15 a15) => _core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11), _js.J2C<J12, C12>(a12), _js.J2C<J13, C13>(a13), _js.J2C<J14, C14>(a14), _js.J2C<J15, C15>(a15));
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
        public RJ Function(J0 a0) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0)));
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
        public RJ Function(J0 a0, J1 a1) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1)));
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
        public RJ Function(J0 a0, J1 a1, J2 a2) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2)));
    }

    public class JSFunction<C0, C1, C2, C3, RC, J0, J1, J2, J3, RJ>
    {
        Func<C0, C1, C2, C3, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, RC, J0, J1, J2, J3, J4, RJ>
    {
        Func<C0, C1, C2, C3, C4, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, RC, J0, J1, J2, J3, J4, J5, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, RC, J0, J1, J2, J3, J4, J5, J6, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, RC, J0, J1, J2, J3, J4, J5, J6, J7, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, C8, RC, J0, J1, J2, J3, J4, J5, J6, J7, J8, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, RC, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, RC, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, RC, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, RC, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11, J12 a12) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11), _js.J2C<J12, C12>(a12)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, RC, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, J13, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11, J12 a12, J13 a13) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11), _js.J2C<J12, C12>(a12), _js.J2C<J13, C13>(a13)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, RC, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, J13, J14, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11, J12 a12, J13 a13, J14 a14) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11), _js.J2C<J12, C12>(a12), _js.J2C<J13, C13>(a13), _js.J2C<J14, C14>(a14)));
    }

    public class JSFunction<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, C15, RC, J0, J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, J13, J14, J15, RJ>
    {
        Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, C15, RC> _core;
        DynamicJSRuntime _js;

        public JSFunction(DynamicJSRuntime js, Func<C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12, C13, C14, C15, RC> core)
        {
            _js = js;
            _core = core;
        }

        [JSInvokable]
        public RJ Function(J0 a0, J1 a1, J2 a2, J3 a3, J4 a4, J5 a5, J6 a6, J7 a7, J8 a8, J9 a9, J10 a10, J11 a11, J12 a12, J13 a13, J14 a14, J15 a15) => _js.C2J<RC, RJ>(_core(_js.J2C<J0, C0>(a0), _js.J2C<J1, C1>(a1), _js.J2C<J2, C2>(a2), _js.J2C<J3, C3>(a3), _js.J2C<J4, C4>(a4), _js.J2C<J5, C5>(a5), _js.J2C<J6, C6>(a6), _js.J2C<J7, C7>(a7), _js.J2C<J8, C8>(a8), _js.J2C<J9, C9>(a9), _js.J2C<J10, C10>(a10), _js.J2C<J11, C11>(a11), _js.J2C<J12, C12>(a12), _js.J2C<J13, C13>(a13), _js.J2C<J14, C14>(a14), _js.J2C<J15, C15>(a15)));
    }
}
