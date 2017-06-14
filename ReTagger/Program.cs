using ReTagger.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReTagger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            if(args.Length > 1 || !File.Exists(args[1]))
            {
                PrintUsage();
                return;
            }

            IEnumerable<Rule> rules;
            try
            {             
                var fileParser = new TextParser();
                var fileReader = new FileReader();
                rules = fileParser.Parse(fileReader.GetLines(args[1]));
            }
            catch (Exception ex)
            {            
                PrintUsage();
                Console.Error.WriteLine(ex.Message);
            }
            


        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: ReTagger UpdateFile [filepattern]");
        }
    }
}