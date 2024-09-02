using System;
using System.IO.Abstractions;

namespace McdaToolkit.FileIO.Path
{
    public class PathFactory
    {
        private readonly IFileSystem _fileSystem;

        public PathFactory(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        
        public Path CreateDefault()
        {
            var dictionary = _fileSystem.Directory.GetCurrentDirectory();
            var fileName = DateTime.Now.ToString("yyyyMMdd");
            
            return Path.Create( fileName,dictionary);
        }
    }
}