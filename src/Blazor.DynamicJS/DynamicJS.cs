using System.Dynamic;

namespace Blazor.DynamicJS
{
    internal class DynamicJS : DynamicObject, IJSSyntax
    {
        DynamicJSRuntime _jsRuntime;
        long _id;
        List<string> _accessor;

        internal DynamicJS(DynamicJSRuntime jsRuntime, long id, List<string> accessor)
        {
            _jsRuntime = jsRuntime;
            _accessor = accessor;
            _id = id;
        }

        //getter
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
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

            _jsRuntime.SetValue(_id, next, value);
            return true;
        }

        //method
        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            var next = _accessor.ToList();
            next.Add(binder.Name);
            result = _jsRuntime.InvokeMethod(_id, next, args ?? new object[0]);
            return true;
        }

        //cast
        public override bool TryConvert(ConvertBinder binder, out object? result)
        {
            result = _jsRuntime.Convert(binder.Type, _id, _accessor);
            return true;
        }

        //function
        public override bool TryInvoke(InvokeBinder binder, object?[]? args, out object? result)
        {
            result = _jsRuntime.InvokeFunctionObject(_id, _accessor, args ?? new object[0]);
            return true;
        }

        //[] = 
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
        {
            result = _jsRuntime.GetIndex(_id, _accessor, indexes);
            return true;
        }

        //[]
        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object? value)
        {
             _jsRuntime.SetIndex(_id, _accessor, indexes, value);
            return true;
        }

        public dynamic New(params object?[] args) => _jsRuntime.New(_accessor, args);

        public async Task<dynamic> NewAsync(params object?[] args) => await _jsRuntime.NewAsync(_accessor, args);

        public async Task<dynamic> InvokeAsync(params object?[] args) => await _jsRuntime.InvokeAsync(_id, _accessor, args);

        internal JSReferenceInfo Marshal() => new JSReferenceInfo { BlazorDynamicJavaScriptUnresolvedNames = _accessor, BlazorDynamicJavaScriptObjectId = _id };
    }
}
