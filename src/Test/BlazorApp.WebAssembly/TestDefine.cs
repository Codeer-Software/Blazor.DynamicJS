using Blazor.DynamicJS;

namespace BlazorApp.WebAssembly
{
    public interface IArrayX
    {
        int this[int _] { get; set; }
        int length { get; set; }


        Task set_ItemAsync(int index, int value);
        Task<int> get_ItemAsync(int index);
    }

    public interface IPinTest
    {
        int a { get; set; }
        string b { get; set; }
        string c(int _);
        IArrayX d { get; set; }

        Task set_aAsync(int _);
        Task<int> get_aAsync();
        Task<string> cAsync(int _);
    }

    [JSCamelCase]
    public interface IArray
    {
        int this[int index] { get; set; }
        int Length { get; set; }
        Task set_ItemAsync(int index, int value);
        Task<int> get_ItemAsync(int index);
    }

    [JSCamelCase]
    public interface IRectangle
    { 
        int Height { get; set; }
        int Width { get; set; }
    }

    [JSCamelCase]
    public interface ITestTargets
    {
        [JSConstructor, JSIgnoreCase]
        IRectangle Rectangle(int h, int w);

        int Sum(params int[] values);
        Task<int> SumAsync(params int[] values);

        int Data { get; set; }

        IArray List { get; set; }
    }
}
