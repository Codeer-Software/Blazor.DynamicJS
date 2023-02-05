namespace BlazorApp.WebAssembly
{
    public interface IArray
    {
        int this[int _] { get; set; }
        int length { get; set; }
    }

    public interface IPinTest
    {
        int a { get; set; }
        string b { get; set; }
        string c(int _);
        IArray d { get; set; }
    }
}
