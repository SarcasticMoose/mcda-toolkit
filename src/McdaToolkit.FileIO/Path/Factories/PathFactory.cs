using System;
using System.IO.Abstractions;

namespace McdaToolkit.FileIO.Path.Factories
{
    public class PathFactory
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _defaultNamePattern = DateTime.UtcNow.ToString("MM-dd-yyyy");

        public PathFactory(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        
        public IToolkitPath Create(string? filename = null, string? directory = null, string? extension = null, bool canOverrideFileNames = true)
        {
            directory ??= "export";
            extension ??= "xml";
            
            if (string.IsNullOrEmpty(filename))
            {
                filename = GenerateFileName(directory,canOverrideFileNames);
            }
            
            return new ToolkitPath(filename,directory,extension);
        }
        
        private string GenerateFileName(string directory, bool canOverrideNames)
        {
            return !canOverrideNames ? _defaultNamePattern : GenerateIncrementedFileName(directory);
        }

        private string GenerateIncrementedFileName(string directory)
        {
            var searchPattern = $"{_defaultNamePattern}*";
            var fileWithTheSameName =_fileSystem.Directory.GetFiles(directory, searchPattern);
            return $"{_defaultNamePattern}_{fileWithTheSameName.Length}";
        }
    }
}