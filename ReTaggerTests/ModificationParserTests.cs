using ReTagger.Parsers;
using ReTagger;
using Xunit;

namespace ReTaggerTests
{
    public class ModificationParserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("@GENRE=THis")]
        [InlineData("GENRE=value")]
        public void ThrowsExceptionInvalidInput(string input)
        {
            var tagParser = new TagParser();
            var modParser = new ModificationParser(tagParser);
            Assert.Throws<ParsingException>(() => modParser.Parse(input));
        }

        [Theory]
        [InlineData("+Genre=value")]
        [InlineData("+Genre=")]
        [InlineData("=GENRE=value")]
        [InlineData("-GENRE=value")]
        public void AcceptsValidInput(string input)
        {
            var tagParser = new TagParser();
            var modParser = new ModificationParser(tagParser);
            Assert.NotNull(modParser.Parse(input));
        }

        [Theory]        
        [InlineData("+Genre=value", Operations.Add)]
        [InlineData("=GENRE=value", Operations.Set)]
        [InlineData("-GENRE=value", Operations.Remove)]
        public void GetsCorrectOperation(string input, Operations operation)
        {
            var tagParser = new TagParser();
            var modParser = new ModificationParser(tagParser);
            Assert.Equal(operation,modParser.Parse(input).Operation);
        }
    }
}
