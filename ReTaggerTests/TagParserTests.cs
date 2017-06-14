using ReTagger.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ReTaggerTests
{
    public class TagParserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid content")]        
        public void ThrowsExceptionWithIncorrctInput(string input)
        {
            var parser = new TagParser();
            Assert.Throws<ParsingException>(() => parser.Parse(input));
        }       
        

        public void AcceptsEmptyValue()
        {
            var parser = new TagParser();
            var tag = parser.Parse("GENRE=");
            Assert.Equal("GENRE", tag.TagName);
            Assert.Equal("", tag.Value);
        }

        public void ReadsCorrectValue()
        {
            var parser = new TagParser();
            var tag = parser.Parse("GENRE=this");
            Assert.Equal("GENRE", tag.TagName);
            Assert.Equal("this", tag.Value);
        }
    }
}
