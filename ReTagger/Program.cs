using ReTagger.DirectoryManipulation;
using ReTagger.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReTagger
{
    class Program
    {
        static bool _simulation = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            if (args.Length < 2 || !File.Exists(args[0]) || !Directory.Exists(args[1]))
            {
                PrintUsage();
                return;
            }

            for (var i = args.Length - 1; i > 1; i--)
            {
                if (args[i].Equals("-simulate", StringComparison.CurrentCultureIgnoreCase))
                {
                    _simulation = true;
                    break;
                }
            }
            IEnumerable<Rule> rules;
            try
            {
                var fileParser = new TextParser();
                var fileReader = new FileReader();
                rules = fileParser.Parse(fileReader.GetLines(args[0]));

                //var recursiveTraversal = args.Length == 3 ? new RecursiveFilteredDirectoryTraversal(args[2]) : new RecursiveFilteredDirectoryTraversal();
                var recursiveTraversal = new RecursiveFilteredDirectoryTraversal("*.flac");

                var count = 0;
                foreach (var file in recursiveTraversal.Traverse(args[1]))
                {
                    if (ProcessFile(file, rules)) count++;
                }
                
                
                Console.Out.WriteLine($"modified {count} files.");
                if (_simulation)
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
                if (!_simulation)
                {
                    tagfile.Save();
                }
                return true;
            }
            return false;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: ReTagger UpdateFile Directory [-simulate]");
        }
    }
}