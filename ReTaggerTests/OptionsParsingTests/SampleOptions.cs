using ReTagger.OptionsParsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReTaggerTests.OptionsParsingTests
{
    public class SampleOptions1
    {
        [Fixed(0)]
        public string Fixed0 { get; set; }
        [Fixed(1)]
        public string Fixed1 { get; set; }
        [Optional(Name ="Option1")]
        public string Optional1 { get; set; }
        [Optional(Name = "Option2")]
        public string Optional2 { get; set; }
        [Flag(Name = "Flag01")]
        public bool Flag1 { get; set; }
        [Flag(Name = "Flag02")]
        public bool Flag2 { get; set; }
    }

    public class SampleOptions2
    {        
        [Optional]
        public string Optional1 { get; set; }
        [Optional]
        public string Optional2 { get; set; }
        [Flag]
        public bool Flag1 { get; set; }
        [Flag]
        public bool Flag2 { get; set; }
    }

    public class SampleOptions3
    {
        [Fixed(0)]
        public string Fixed0 { get; set; }
        [Fixed(0)]
        public string Fixed1 { get; set; }
    }

}
