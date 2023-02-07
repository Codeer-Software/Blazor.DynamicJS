namespace Blazor.DynamicJS
{
    public class JSConstructorAttribute : Attribute { }

    //todo
    public class JSProperty : Attribute { }

    //todo
    public class JSIndexProperty : Attribute { }

    public class JSCamelCaseAttribute : Attribute { }
    
    public class CSNameAttribute : Attribute { }

    //todo
    public class JSNameAttribute : Attribute 
    {
        public string Name { get; }
        public JSNameAttribute(string name) => Name = name;
    }
}
