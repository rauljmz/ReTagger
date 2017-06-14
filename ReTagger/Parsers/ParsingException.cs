using System;

namespace ReTagger.Parsers
{
    public class ParsingException : Exception
    {
        public string OffendingInput { get; set; }
    }
}
