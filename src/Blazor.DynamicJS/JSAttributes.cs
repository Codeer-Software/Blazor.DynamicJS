namespace Blazor.DynamicJS
{
    public class JSConstructorAttribute : Attribute
    {
        public string Name { get; } = string.Empty;
        public JSConstructorAttribute() { }
        public JSConstructorAttribute(string name) { }
    }

    public class JSProperty : Attribute
    {
        public string Name { get; } = string.Empty;
        public JSProperty() { }
        public JSProperty(string name) { }
    }

    public class JSIndexProperty : Attribute
    {
        public string Name { get; } = string.Empty;
        public JSIndexProperty() { }
        public JSIndexProperty(string name) { }
    }

    public class JSNameAttribute : Attribute
    {
        public string Name { get; }
        public JSNameAttribute(string name) => Name = name;
    }

    public class JSCamelCaseAttribute : Attribute { }
    
    public class CSNameAttribute : Attribute { }

}
