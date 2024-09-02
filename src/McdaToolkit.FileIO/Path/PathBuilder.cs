using System;
using System.IO.Abstractions;
using McdaToolkit.Configuration;

namespace McdaToolkit.FileIO.Path
{
    internal sealed class PathBuilder : IPathBuilder
    {
        private IConfigurator _configurator;

        public PathBuilder(IConfigurator? configurator = null)
        {
            _configurator = configurator ?? new Configurator();
        }

        public IPathBuilder WithDirectory(string directoryName)
        {
            _configurator.AddOption(new ConfigOption<string>(ConfigKeysConstants.PathDictionaryName, directoryName));
            return this;
        }
        
        public IPathBuilder WithFileName(string fileName)
        {
            _configurator.AddOption(new ConfigOption<string>(ConfigKeysConstants.PathFileName, fileName));
            return this;
        }

        public Path Build()
        {
            var fileName = _configurator.GetOption<string>(ConfigKeysConstants.PathFileName)?.Value;
            var directoryName = _configurator.GetOption<string>(ConfigKeysConstants.PathDictionaryName)?.Value;
            var fileSystem = _configurator.GetOption<IFileSystem>(ConfigKeysConstants.FileSystem)?.Value ?? new FileSystem();;

            if (fileName is null)
            {
                fileName = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (directoryName is null)
            {
                directoryName = fileSystem.Directory.GetCurrentDirectory();
            }
            
            var path = Path.Create(fileName,directoryName);
            return path;
        }
    }
}