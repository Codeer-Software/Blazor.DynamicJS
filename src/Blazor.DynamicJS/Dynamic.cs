using System.Dynamic;
using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    //adjust dynamic args

    public class DynamicJSRuntime : IDisposable, IAsyncDisposable
    {
        Guid _guid;
        public DynamicJSRuntime() => _guid = Guid.NewGuid();

        internal List<IDisposable> Disposables { get; } = new List<IDisposable>();

        public void Dispose()
        {
        }

        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }
    }

    public static class JSRuntimeExtentions
    {
        public static dynamic Window(this IJSRuntime jsRuntime, DynamicJSRuntime connection) => new DynamicJSReference(jsRuntime, 0, new List<string>());

        public static dynamic New(this IJSRuntime jsRuntime, DynamicJSRuntime connection) => new DynamicJSNew(jsRuntime);


        public static dynamic ToJSFuction(this IJSRuntime jsRuntime, DynamicJSRuntime connection, Action action)
            => ToJSFunctionCore(jsRuntime, connection, DotNetObjectReference.Create(new JSFunctionVoid(action)));

        public static dynamic ToJSFuction<T0>(this IJSRuntime jsRuntime, DynamicJSRuntime connection, Action<T0> action)
            => ToJSFunctionCore(jsRuntime, connection, DotNetObjectReference.Create(new JSFunctionVoid<T0>(action)));

        public static dynamic ToJSFuction<T0, T1>(this IJSRuntime jsRuntime, DynamicJSRuntime connection, Action<T0, T1> action)
            => ToJSFunctionCore(jsRuntime, connection, DotNetObjectReference.Create(new JSFunctionVoid<T0, T1>(action)));

        public static dynamic ToJSFuction<T0, T1, T2>(this IJSRuntime jsRuntime, DynamicJSRuntime connection, Action<T0, T1, T2> action)
            => ToJSFunctionCore(jsRuntime, connection, DotNetObjectReference.Create(new JSFunctionVoid<T0, T1, T2>(action)));



        public static dynamic ToJSFuction<R>(this IJSRuntime jsRuntime, DynamicJSRuntime connection, Func<R> func)
            => ToJSFunctionCore(jsRuntime, connection, DotNetObjectReference.Create(new JSFunction<R>(func)));

        public static dynamic ToJSFuction<T0, R>(this IJSRuntime jsRuntime, DynamicJSRuntime connection, Func<T0, R> func)
            => ToJSFunctionCore(jsRuntime, connection, DotNetObjectReference.Create(new JSFunction<T0, R>(func)));

        public static dynamic ToJSFuction<T0, T1, R>(this IJSRuntime jsRuntime, DynamicJSRuntime connection, Func<T0, T1, R> func)
            => ToJSFunctionCore(jsRuntime, connection, DotNetObjectReference.Create(new JSFunction<T0, T1, R>(func)));

        public static dynamic ToJSFuction<T0, T1, T2, R>(this IJSRuntime jsRuntime, DynamicJSRuntime connection, Func<T0, T1, T2, R> func)
            => ToJSFunctionCore(jsRuntime, connection, DotNetObjectReference.Create(new JSFunction<T0, T1, T2, R>(func)));




        private static dynamic ToJSFunctionCore(IJSRuntime jsRuntime, DynamicJSRuntime connection, IDisposable objRef)
        {
            connection.Disposables.Add(objRef);

            //TODO adjust dynamic args

            var sync = (IJSInProcessRuntime)jsRuntime;
            var id = sync.Invoke<long>("window.BlazorDynamicJavaScriptHelper.createFunction", objRef, "Function");
            return new DynamicJSReference(jsRuntime, id, new List<string>());
        }
    }


    public interface IJSReference { }

    public class DynamicJSReference : DynamicObject, IJSReference
    {
        IJSRuntime _jsRuntime;
        long _id;
        List<string> _accessor;

        internal DynamicJSReference(IJSRuntime jsRuntime, long id, List<string> accessor)
        {
            _jsRuntime = jsRuntime;
            _accessor = accessor;
            _id = id;
        }

        //getter
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_id == 0 && _accessor.Count == 0 && binder.Name == "window")
            {
                result = this;
                return true;
            }

            var next = _accessor.ToList();
            next.Add(binder.Name);
            result = new DynamicJSReference(_jsRuntime, _id, next);
            return true;
        }

        //setter
        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            var next = _accessor.ToList();
            next.Add(binder.Name);

            if (value is DynamicJSReference r) value = r.Marshal();
            
            var sync = (IJSInProcessRuntime)_jsRuntime;
            sync.InvokeVoid("window.BlazorDynamicJavaScriptHelper.setProperty", _id, next, value);
            return true;
        }

        //method
        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            var next = _accessor.ToList();
            next.Add(binder.Name);

            args = args ?? new object[0];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJSReference r) args[i] = r.Marshal();
            }

            var sync = (IJSInProcessRuntime)_jsRuntime;
            var id = sync.Invoke<long>("window.BlazorDynamicJavaScriptHelper.invokeMethod", _id, next, args);
            result = new DynamicJSReference(_jsRuntime, id, new List<string>());
            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object? result)
        {
            var converter = typeof(Converter<>).MakeGenericType(binder.Type);
            result = converter.GetMethod("Convert")!.Invoke(null, new object[] { _jsRuntime, _id, _accessor });
            return true;
        }

        //TODO
        /*
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
        {
            Debug.WriteLine($"this.{string.Join(".", _code)}[{string.Join(".", indexes)}]");
            result = new DynamicJSObject(_jsRuntime, 2);
            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object? value)
        {
            Debug.WriteLine($"this.{string.Join(".", _code)}[{string.Join(".", indexes)}] = {value}");
            return true;
        }
        */
        
        //new
        public override bool TryInvoke(InvokeBinder binder, object?[]? args, out object? result)
        {
            args = args ?? new object[0];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJSReference r) args[i] = r.Marshal();
            }

            var sync = (IJSInProcessRuntime)_jsRuntime;
            var id = sync.Invoke<long>("window.BlazorDynamicJavaScriptHelper.invokeMethod", _id, _accessor, args);
            result = new DynamicJSReference(_jsRuntime, id, new List<string>());
            return true;
        }


        public ReferenceInfo Marshal() => new ReferenceInfo { BlazorDynamicJavaScriptUnresolvedNames = _accessor, BlazorDynamicJavaScriptObjectId = _id };

        public class Converter<T>
        {
            public static T Convert(IJSRuntime jsRuntime, long id, List<string> accessor)
            {
                var sync = (IJSInProcessRuntime)jsRuntime;
                return sync.Invoke<T>("window.BlazorDynamicJavaScriptHelper.getObject", id, accessor);
            }
        }
    }

    public class ReferenceInfo
    {
        public long BlazorDynamicJavaScriptObjectId { get; set; }
        public List<string> BlazorDynamicJavaScriptUnresolvedNames { get; set; } = new List<string>();
    }






    public class DynamicJSNew : DynamicObject
    {
        IJSRuntime _jsRuntime;
        List<string> _accessor;

        internal DynamicJSNew(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _accessor = new List<string>();
        }

        internal DynamicJSNew(IJSRuntime jsRuntime, List<string> accessor)
        {
            _jsRuntime = jsRuntime;
            _accessor = accessor;
        }

        //getter
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_accessor.Count == 0 && binder.Name == "window")
            {
                result = this;
                return true;
            }

            var next = _accessor.ToList();
            next.Add(binder.Name);
            result = new DynamicJSNew(_jsRuntime, next);
            return true;
        }

        //method
        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            var next = _accessor.ToList();
            next.Add(binder.Name);

            args = args ?? new object[0];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJSReference r) args[i] = r.Marshal();
            }

            var sync = (IJSInProcessRuntime)_jsRuntime;
            var id = sync.Invoke<long>("window.BlazorDynamicJavaScriptHelper.createObject", next, args);
            result = new DynamicJSReference(_jsRuntime, id, new List<string>());
            return true;
        }
    }

}
