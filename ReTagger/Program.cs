using ReTagger.DirectoryManipulation;
using ReTagger.OptionsParsing;
using ReTagger.Parsers;
using System;
using System.Collections.Generic;

namespace ReTagger
{
    class Program
    {
        static ApplicationOptions _applicationOptions;

        static void Main(string[] args)
        {
            Console.WriteLine("ReTagger");

            try
            {
                _applicationOptions = OptionsParser.Parse<ApplicationOptions>(args);
                if (!_applicationOptions.Validate())
                {
                    Console.WriteLine("One of the options is incorrect");
                    Console.WriteLine(_applicationOptions.Usage());
                    return;
                }
            }
            catch (OptionsParsingException ex)
            {
                Console.WriteLine(ex.Message);
                return;                
            }

            

            IEnumerable<Rule> rules;
            try
            {
                var fileParser = new TextParser();
                var fileReader = new FileReader();
                rules = fileParser.Parse(fileReader.GetLines(_applicationOptions.UpdateFile));

                var recursiveTraversal = new RecursiveFilteredDirectoryTraversal(_applicationOptions.Filter);

                var count = 0;
                foreach (var file in recursiveTraversal.Traverse(_applicationOptions.Directory))
                {
                    if (ProcessFile(file, rules)) count++;
                }
                
                
                Console.Out.WriteLine($"modified {count} files.");
                if (_applicationOptions.Simulate)
                {
                    Console.Out.WriteLine("in SIMULATION mode no changes made");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.Write(ex.StackTrace);
                Console.Error.WriteLine(ex.Message);
            }
        }

        private static bool ProcessFile(string file, IEnumerable<Rule> rules)
        {
            var needsToSave = false;
            var tagfile = TagLib.File.Create(file);
            foreach (var rule in rules)
            {
                needsToSave |= rule.Apply(tagfile);
            }

            if (needsToSave)
            {
                Console.WriteLine($"Saving file {file}");
                if (!_applicationOptions.Simulate)
                {
                    tagfile.Save();
                }
                return true;
            }
            return false;
        }
        
    }
}