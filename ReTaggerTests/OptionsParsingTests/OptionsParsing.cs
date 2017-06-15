using ReTagger.OptionsParsing;
using System;
using Xunit;

namespace ReTaggerTests.OptionsParsingTests
{
    public class OptionsParsing
    {
        [InlineData("")]        
        [InlineData("f1","f2","-Option2")]
        [Theory]
        public void ShouldThrowExceptionMismatchedNumberArguments(params string[] array)
        {
            Assert.Throws<OptionsParsingException>(() => OptionsParser.Parse<SampleOptions1>( array));
        }

        [Fact]
        public void ShouldThrowAddingTwoFixedPropertiesSameIndex()
        {
            Assert.Throws<Exception>(() => OptionsParser.Parse<SampleOptions3>("",""));
        }

        [Theory]
        [InlineData("fixed","fixed")]
        [InlineData("fixed", "fixed", "notused")]
        [InlineData("fixed", "fixed", "-Option1", "value")]
        [InlineData("fixed", "fixed", "-Option1", "-Flag01")]
        public void FixedPropertyIsPopulated(params string[] array)
        {
            var options = OptionsParser.Parse<SampleOptions1>(array);
            Assert.Equal(array[0], options.Fixed0);
            Assert.Equal(array[1], options.Fixed1);
        }

        [Theory]               
        [InlineData("fixed", "fixed", "-Option1", "value")]
        [InlineData("fixed", "fixed", "-Option1", "value", "-Flag01")]
        public void OptionPropertyIsPopulated(params string[] array)
        {
            var options = OptionsParser.Parse<SampleOptions1>(array);
            Assert.Equal(array[3], options.Optional1);
            Assert.Equal(array[3], options.Optional1);
        }

        [Theory]
        [InlineData("fixed", "fixed", "-Flag01")]
        [InlineData("fixed", "fixed", "-Option1", "value", "-Flag01")]
        public void FlagPropertyIsPopulated(params string[] array)
        {
            var options = OptionsParser.Parse<SampleOptions1>(array);
            Assert.True(options.Flag1);
        }

        [Theory]
        [InlineData("fixed", "fixed", "-Flag1")]
        [InlineData("fixed", "fixed", "-Option1", "value", "-Flag1")]
        public void FlagPropertyIsPopulatedUsingDefaultName(params string[] array)
        {
            var options = OptionsParser.Parse<SampleOptions2>(array);
            Assert.True(options.Flag1);
        }


        [Theory]
        [InlineData("fixed", "fixed", "-Optional1", "value")]
        [InlineData("fixed", "fixed", "-Optional1", "value", "-Flag01")]
        public void OptionPropertyIsPopulatedUsingDefaultName(params string[] array)
        {
            var options = OptionsParser.Parse<SampleOptions2>(array);
            Assert.Equal(array[3], options.Optional1);
            Assert.Equal(array[3], options.Optional1);
        }

    }
}
