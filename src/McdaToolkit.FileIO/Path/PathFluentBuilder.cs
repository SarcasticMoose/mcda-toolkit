using System.IO.Abstractions;
using McdaToolkit.Configuration;
using McdaToolkit.FileIO.Configuration;
using McdaToolkit.FileIO.Path.Factories;

namespace McdaToolkit.FileIO.Path
{
    internal sealed class PathFluentBuilder : IPathFluentBuilder
    {
        private readonly ToolkitConfiguration _configurator;

        public bool OverrideFilesWithSameName { get; set; } = true;

        public PathFluentBuilder(IToolkitConfiguration configuratorToolkit)
        {
            _configurator = new McdaToolkit.Configuration.ToolkitConfiguration();
        }
        
        public PathFluentBuilder()
        {
            _configurator = new ToolkitConfiguration();
        }

        public IPathFluentBuilder WithDirectory(string directoryName)
        {
            _configurator.AddOption(new ConfigOption<string>(PathConfigKeys.Directory, directoryName));
            return this;
        }
        
        public IPathFluentBuilder WithFileName(string fileName)
        {
            _configurator.AddOption(new ConfigOption<string>(PathConfigKeys.FileName, fileName));
            return this;
        }
        
        public IToolkitPath Build()
        {
            var fileName = _configurator.GetOptionOrDefault<string>(PathConfigKeys.FileName)?.Value;
            var directoryName = _configurator.GetOptionOrDefault<string>(PathConfigKeys.Directory)?.Value;
            var fileSystem = _configurator.GetOption<IFileSystem>(FileIoConfigKeys.FileSystem).Value;

            var path = new PathFactory(fileSystem).Create(fileName, directoryName);
            return path;
        }
    }
}