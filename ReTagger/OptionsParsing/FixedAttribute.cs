using System;

namespace ReTagger.OptionsParsing
{
    public class FixedAttribute : Attribute
    {
        public int Position { get; set; }

        public FixedAttribute(int c)
        {
            Position = c;
        }
    }
}
