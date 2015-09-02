using System;

namespace MVCWebHelpers
{
    public class PagingModel
    {
        public int MaxPages { get; set; }
        public int CurrentPage { get; set; }
        public Func<int, string> UrlGeneratorFunction { get; set; }
    }
}