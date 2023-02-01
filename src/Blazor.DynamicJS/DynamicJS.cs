using System.Dynamic;
using Microsoft.JSInterop;

namespace Blazor.DynamicJS
{
    internal class DynamicJS : DynamicObject, IJSSyntax
    {
        IJSRuntime _jsRuntime;
        long _id;
        List<string> _accessor;

        internal DynamicJS(IJSRuntime jsRuntime, long id, List<string> accessor)
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
            result = new DynamicJS(_jsRuntime, _id, next);
            return true;
        }

        //setter
        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            var next = _accessor.ToList();
            next.Add(binder.Name);

            if (value is DynamicJS r) value = r.Marshal();
            
            var sync = (IJSInProcessRuntime)_jsRuntime;
            sync.InvokeVoid("window.BlazorDynamicJavaScriptHelper.setProperty", _id, next, value);
            return true;
        }

        //method
        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            var next = _accessor.ToList();
            next.Add(binder.Name);

            //adjust funcobjects
            args = args ?? new object[0];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJS r) args[i] = r.Marshal();
            }

            var sync = (IJSInProcessRuntime)_jsRuntime;
            var id = sync.Invoke<long>("window.BlazorDynamicJavaScriptHelper.invokeMethod", _id, next, args);
            result = new DynamicJS(_jsRuntime, id, new List<string>());
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
        
        //function
        public override bool TryInvoke(InvokeBinder binder, object?[]? args, out object? result)
        {
            args = args ?? new object[0];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJS r) args[i] = r.Marshal();
            }

            var sync = (IJSInProcessRuntime)_jsRuntime;
            var id = sync.Invoke<long>("window.BlazorDynamicJavaScriptHelper.invokeMethod", _id, _accessor, args);
            result = new DynamicJS(_jsRuntime, id, new List<string>());
            return true;
        }
        

        ReferenceInfo Marshal() => new ReferenceInfo { BlazorDynamicJavaScriptUnresolvedNames = _accessor, BlazorDynamicJavaScriptObjectId = _id };

        public class Converter<T>
        {
            public static T Convert(IJSRuntime jsRuntime, long id, List<string> accessor)
            {
                var sync = (IJSInProcessRuntime)jsRuntime;
                return sync.Invoke<T>("window.BlazorDynamicJavaScriptHelper.getObject", id, accessor);
            }
        }

        internal DynamicJS New(object?[] args)
        {
            args = args ?? new object[0];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is DynamicJS r) args[i] = r.Marshal();
            }

            var sync = (IJSInProcessRuntime)_jsRuntime;
            var id = sync.Invoke<long>("window.BlazorDynamicJavaScriptHelper.createObject", _accessor, args);
            return new DynamicJS(_jsRuntime, id, new List<string>());
        }

        public class ReferenceInfo
        {
            public long BlazorDynamicJavaScriptObjectId { get; set; }
            public List<string> BlazorDynamicJavaScriptUnresolvedNames { get; set; } = new List<string>();
        }
    }
}
