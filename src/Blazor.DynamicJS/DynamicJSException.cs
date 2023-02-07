using static Blazor.DynamicJS.ExceptionMessageHelper;

namespace Blazor.DynamicJS
{
    static class ExceptionMessageHelper
    {
        public static string ToReturnType(Type type) => type == typeof(object) ? "dynamic" : type.Name;

        public static string ToName(long objId, List<string> accessor)
        {
            objId.ToString();//todo
            return string.Join(".", accessor);
        }

        public static string ToValue(object? value)
            => value == null ? "null" :
               value.GetType() == typeof(object) ? "dynamic" :
                value.ToString()!;

        public static string ToArgs(object?[] args) => string.Join(", ", args.Select(e => ToValue(e)));
    }

    public class DynamicJSPropertyException : Exception
    {
        public DynamicJSPropertyException(Type type, long objId, List<string> accessor)
            : base(ToReturnType(type) + " " + ToName(objId, accessor) + " : there is an error somewhere in names or return value type.") { }

        public DynamicJSPropertyException(long objId, List<string> accessor, object? value)
            : base(ToName(objId, accessor) + " = " + ToValue(value) + " : there is an error somewhere in names or value.") { }
    }

    public class DynamicJSFunctionException : Exception
    {
        public DynamicJSFunctionException(long objId, List<string> accessor, object?[] args)
            : base(ToName(objId, accessor) + " = " + ToArgs(args) + " : there is an error somewhere in names or return value type or arguments.") { }
    }

    public class DynamicJSIndexPropertyException : Exception
    {
        public DynamicJSIndexPropertyException(long objId, List<string> accessor, object[] index)
            : base(ToName(objId, accessor) +"[" + ToValue(index[0]) + "] : there is an error somewhere in names or index or return value type.") { }

        public DynamicJSIndexPropertyException(long objId, List<string> accessor, object[] index, object? value)
            : base(ToName(objId, accessor) + "[" + ToValue(index[0]) + "]" + " = " + ToValue(value) + " : there is an error somewhere in names or index or value.") { }
    }

    public class DynamicJSNewException : Exception
    {
        public DynamicJSNewException(string message) : base(message) { }

        public DynamicJSNewException(long objId, List<string> accessor, object?[] args)
            : base("new " + ToName(objId, accessor) + " = " + ToArgs(args) + " : there is an error somewhere in names or arguments.") { }
    }
}
