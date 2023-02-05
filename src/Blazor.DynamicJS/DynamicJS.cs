using System;
using System.Dynamic;
using System.Reflection;

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

        public TInterface Pin<TInterface>() => DynamicJSProxy<TInterface>.CreateEx(this);

        public async Task SetValueAsync(object? value) => await _jsRuntime.SetValueAsync(_id, _accessor, value);
        public async Task SetIndexValueAsync(object idnex, object? value) => await _jsRuntime.SetIndexAsync(_id, _accessor, new[] { idnex }, value);
        public async Task<T> GetValueAsync<T>() => (T)(object)(await _jsRuntime.ConvertAsync(typeof(T), _id, _accessor))!;
        public async Task<T> GetIndexValueAsync<T>(object idnex)
        {
            var x = await _jsRuntime.GetIndexAsync(_id, _accessor, new[] { idnex });
            return (T)(object)(await _jsRuntime.ConvertAsync(typeof(T), x._id, x._accessor))!;
        }

        internal JSReferenceJsonableData ToJsonable()
            => new JSReferenceJsonableData { BlazorDynamicJavaScriptUnresolvedNames = _accessor, BlazorDynamicJavaScriptObjectId = _id };

        internal object? InvokeProxyMethod(MethodInfo? targetMethod, object?[]? args)
        {
            var name = targetMethod!.Name;
            var next = _accessor.ToList();

            if (name == "set_Item")
            {
                _jsRuntime.SetIndex(_id, _accessor, new[] { args![0]! }, args[1]);
                return null;
            }
            if (name.StartsWith("set_"))
            {
                next.Add(name.Substring("set_".Length));
                _jsRuntime.SetValue(_id, next, args![0]);
                return null;
            }

            DynamicJS result;
            if (name == "get_Item")
            {
                result = _jsRuntime.GetIndex(_id, _accessor, new[] { args![0]! });
            }
            else if (name.StartsWith("get_"))
            {
                next.Add(name.Substring("get_".Length));
                result = new DynamicJS(_jsRuntime, _id, next);
            }
            else
            {
                next.Add(name);
                result = _jsRuntime.InvokeMethod(_id, next, args ?? new object[0]);
                if (targetMethod.ReturnType == typeof(void)) return null;
            }

            if (targetMethod.ReturnType.IsInterface)
            {
                return ReflectionHelper.InvokeGenericStaticMethod(
                    typeof(DynamicJSProxy<>), new[] { targetMethod.ReturnType },
                    "CreateEx", new object[] { result });
            }

            return _jsRuntime.Convert(targetMethod.ReturnType, result._id, result._accessor);
        }

    }
}
