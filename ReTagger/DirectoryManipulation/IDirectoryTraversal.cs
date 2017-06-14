using System.Collections.Generic;

namespace ReTagger.DirectoryManipulation
{
    interface IDirectoryTraversal
    {
        IEnumerable<string> Traverse(string root);
    }
}
