using System.Collections.Generic;
using System.IO;

namespace ReTagger
{
    public class FileReader
    {
        public IEnumerable<string> GetLines(string file)
        {
            return File.ReadLines(file);
        }
    }
}
