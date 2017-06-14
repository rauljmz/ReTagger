using System.Collections.Generic;
using System.IO;

namespace ReTagger.DirectoryManipulation
{
    public class RecursiveFilteredDirectoryTraversal : IDirectoryTraversal
    {
        private readonly string _filter;

        public RecursiveFilteredDirectoryTraversal():this("*")
        {

        }

        public RecursiveFilteredDirectoryTraversal(string filter)
        {
            _filter = filter;
        }

        public IEnumerable<string> Traverse(string root)
        {
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException(root);
            }
            foreach (var dir in Directory.GetDirectories(root))
            {
                foreach(var file in Traverse(dir))
                {
                    yield return file;
                }
            }

            foreach (var file in Directory.GetFiles(root,_filter))
            {
                yield return file;
            }
        }
    }
}
