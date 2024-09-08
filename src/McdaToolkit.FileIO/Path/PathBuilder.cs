using System.IO.Abstractions;
using McdaToolkit.Configuration;
using McdaToolkit.FileIO.Path.Factories;

namespace McdaToolkit.FileIO.Path
{
    internal sealed class PathBuilder : IPathBuilder
    {
        private Configurator _configurator;

        public bool OverrideFilesWithSameName { get; set; } = true;

        public PathBuilder(IReadOnlyConfigurator configuratorBase)
        {
            _configurator = new Configurator(configuratorBase.GetOptions());
        }
        
        public PathBuilder()
        {
            _configurator = new Configurator();
        }

        public IPathBuilder WithDirectory(string directoryName)
        {
            _configurator.AddOption(new ConfigOption<string>("path.directory", directoryName));
            return this;
        }
        
        public IPathBuilder WithFileName(string fileName)
        {
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