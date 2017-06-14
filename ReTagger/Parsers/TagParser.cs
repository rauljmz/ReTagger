using System;
using System.Collections.Generic;
using System.Text;

namespace ReTagger.Parsers
{
    public class TagParser
    {
        public Tag Parse(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var split = input.Split('=');
                if (split.Length >= 2)
                {
                    return new Tag()
                    {
                        TagName = split[0].ToUpperInvariant(),
                        Value = split[1]
                    };
                }
            }
            throw new ParsingException() { OffendingInput = input };
        }
    }
}
