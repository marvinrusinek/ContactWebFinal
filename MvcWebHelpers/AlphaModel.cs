using System;
using System.Collections.Generic;

namespace MVCWebHelpers
{
    public class AlphaModel
    {
        public string SelectedLetter { get; set; }
        public Func<string, string> UrlGeneratorFunction { get; set; }
        public Dictionary<string, int> LetterDictionary { get; set; }
    }
}