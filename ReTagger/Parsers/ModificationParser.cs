using System;
using System.Collections.Generic;
using System.Text;

namespace ReTagger.Parsers
{
    public class ModificationParser
    {
        private readonly TagParser _tagParser;
        public ModificationParser(TagParser tagparser)
        {
            _tagParser = tagparser;
        }

        public Modification Parse(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var mod = new Modification()
                {
                    Tag = _tagParser.Parse(input.Substring(1))
                };
                switch (input[0])
                {
                    case '+':
                        mod.Operation = Operations.Add;
                        break;
                    case '=':
                        mod.Operation = Operations.Set;
                        break;
                    case '-':
                        mod.Operation = Operations.Remove;
                        break;
                    default:
                        throw new ParsingException(input);
                }
                return mod;
            }
            throw new ParsingException(input);
        }
    }
}
