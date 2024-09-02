using System;

namespace McdaToolkit.FileIO.Path
{
    public class Path
    {
        private Path(string fileName, string directoryPath)
        {
            FileName = fileName;
            DirectoryName = directoryPath;
        }
        
        public static Path Create(string fileName, string directoryPath)
        {
            return new Path(fileName, directoryPath);
        }
        
        public string DirectoryName { get; private set; }
        public string FileName { get; private set; }
    }
}