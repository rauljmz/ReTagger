using Xunit;
using ReTagger;
using System.Collections.Generic;
using ReTagger.Parsers;

namespace ReTaggerTests
{
    public class FileParserTests
    {
        private static IEnumerable<Rule> ParseLines(string data)
        {
            var lines = data.Split('\n');
            var parser = new TextParser();
            var result = parser.Parse(lines);
            return result;
        }

        [Theory]
        [InlineData("\n\n")]
        [InlineData("\nThis is ignored\n")]
        public void DiscardsEmptyOrNonValidLines(string data)
        {
            var result = ParseLines(data);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData("@Genre")]
        [InlineData("@Genre=Piano\n")]
        [InlineData("@Genre=Piano\n&GENRE=Music")]
        [InlineData("@Genre=Piano\n+GENRE=Music\n=ANOTHER=value")]
        [InlineData("@Genre=Piano\n+GENRE=Music\n\nOthercontentignored\n=ANOTHER=value")]
        public void ThrowsParsingErrorIncompleteLines(string data)
        {
            Assert.Throws<ParsingException>( () => ParseLines(data) );
        }

        [Theory]
        [InlineData("@Genre=Piano\n=GENRE=Music")]
        [InlineData("@Genre=Piano\n=GENRE=Music\n+GENRE=Piano")]
        [InlineData("@Genre=Piano\n=GENRE=Music")]
        [InlineData("@Genre=Piano\n=GENRE=Music\n+GENRE=Piano")]
        public void CapturesDataRule(string data)
        {
            var result = ParseLines(data);
            Assert.NotEmpty(result);
        }
        
    }
}
