using System;

namespace ReTagger.OptionsParsing
{
    public class FlagAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
