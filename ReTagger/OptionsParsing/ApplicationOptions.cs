using System.IO;

namespace ReTagger.OptionsParsing
{
    public class ApplicationOptions
    {
        [Fixed(0)]
        public string UpdateFile { get; set; }
        [Fixed(1)]
        public string Directory { get; set; }
        [Optional]
        public string Filter { get; set; }
        [Flag]
        public bool Simulate { get; set; }

        public ApplicationOptions()
        {
            Filter = "*.flac";
            Simulate = false;
        }

        public bool Validate()
        {
            return System.IO.Directory.Exists(Directory)
                && File.Exists(UpdateFile);
        }

        public string Usage()
        {
            return "ReTagger <UpdateFile> <Directory> [-Filter <filter>] [-Simulate]";
        }

        
    }
}
