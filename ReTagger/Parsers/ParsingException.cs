using System;

namespace ReTagger.Parsers
{
    public class ParsingException : Exception
    {
        public ParsingException(string input) : base($"there is a parsing errror in line {input}")
        {

        }
        
    }
}
