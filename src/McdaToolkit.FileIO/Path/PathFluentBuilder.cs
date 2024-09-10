using System.IO.Abstractions;
using McdaToolkit.Configuration;
using McdaToolkit.FileIO.Path.Factories;

namespace McdaToolkit.FileIO.Path
{
    internal sealed class PathFluentBuilder : IPathFluentBuilder
    {
        private Configurator _configurator;

        public bool OverrideFilesWithSameName { get; set; } = true;

        public PathFluentBuilder(IBaseConfiguration configuratorBase)
        {
            _configurator = new Configurator(configuratorBase.GetOptions());
        }
        
        public PathFluentBuilder()
        {
            _configurator = new Configurator();
        }

        public IPathFluentBuilder WithDirectory(string directoryName)
        {
            _configurator.AddOption(new ConfigOption<string>("path.directory", directoryName));
            return this;
        }
        
        public IPathFluentBuilder WithFileName(string fileName)
        {
            var fileSystem = _configurator.GetOption<IFileSystem>("filesystem").Value;
            _configurator.AddOption(new ConfigOption<string>("path.filename", fileName));
            return this;
        }
        
        public IToolkitPath Build()
        {
            var fileName = _configurator.GetOptionOrDefault<string>("path.filename")?.Value;
            var directoryName = _configurator.GetOptionOrDefault<string>("path.directory")?.Value;
            var fileSystem = _configurator.GetOption<IFileSystem>("filesystem").Value;

            var path = new PathFactory(fileSystem).Create(fileName, directoryName);
            return path;
        }
    }
}