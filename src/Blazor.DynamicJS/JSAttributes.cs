namespace Blazor.DynamicJS
{
    public class JSConstructorAttribute : Attribute
    {
        public string Name { get; } = string.Empty;
        public JSConstructorAttribute() { }
        public JSConstructorAttribute(string name) => Name = name;
    }

    public class JSPropertyAttribute : Attribute
    {
        public string Name { get; } = string.Empty;
        public JSPropertyAttribute() { }
        public JSPropertyAttribute(string name) => Name = name;
    }

    public class JSIndexPropertyAttribute : Attribute { }

    public class JSNameAttribute : Attribute
    {
        public string Name { get; }
        public JSNameAttribute(string name) => Name = name;
    }

    public class JSCamelCaseAttribute : Attribute { }
    
    public class CSNameAttribute : Attribute { }

}
