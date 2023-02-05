﻿using Blazor.DynamicJS;

namespace BlazorApp.WebAssembly
{
    public interface IArray
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
        IArray d { get; set; }

        Task set_aAsync(int _);
        Task<int> get_aAsync();
        Task<string> cAsync(int _);
    }

    public interface IRectangle
    { 
        int height { get; set; }
        int width { get; set; }
    }

    public interface ITestTargets
    {
        [NewSyntax]
        IRectangle Rectangle(int h, int w);
    }
}
