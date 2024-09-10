using System.Collections.Generic;
using System.IO.Abstractions;
using McdaToolkit.Configuration;
using McdaToolkit.FileIO.Path;
using McdaToolkit.FileIO.Path.Factories;
using McdaToolkit.FileIO.Writer;
using McdaToolkit.Serializer;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter.Configuration
{
    internal class ExporterConfiguration : IExporterConfiguration
    {
        private readonly Configurator _configurator = new Configurator();

        public ExporterConfiguration()
        {
            SetFileWriter();
        }

        public IBaseConfiguration GetConfiguration() => _configurator;
        
        public IToolkitPath GetPath()
        {
            var pathConfig = _configurator.GetOptionOrDefault<IToolkitPath>("exporter.path");
            if (pathConfig != null)
            {
                return pathConfig.Value;
            }

            var defaultPath = new PathFactory(GetFileSystem()).Create();
            SetPath(defaultPath);
            return defaultPath;
        }

        public void SetPath(IToolkitPath path)
        {
            _configurator.AddOption(new ConfigOption<IToolkitPath>("exporter.path", path));
        }
        
        public ISerializer GetSerializer()
        {
            var serializerConfig = _configurator.GetOptionOrDefault<ISerializer>("exporter.serializer");
            if (serializerConfig != null)
            {
                return serializerConfig.Value;
            }
            var serializer = new DefaultSerializer();
            SetSerializer(serializer);
            return serializer;
        }
        
        public void SetSerializer(ISerializer serializer)
        {
            _configurator.AddOption(new ConfigOption<ISerializer>("exporter.serializer", serializer));
        }

        public IFileSystem GetFileSystem()
        {
            var fileSystemConfig = _configurator.GetOptionOrDefault<IFileSystem>("filesystem");
            if (fileSystemConfig is null)
            {
                SetFileSystem(new FileSystem());
            }
            return _configurator.GetOption<IFileSystem>("filesystem").Value;
        }

        public IFileWriter GetFileWriter()
        {
            return _configurator.GetOption<IFileWriter>("exporter.filewriter").Value;
        }

        private void SetFileSystem(IFileSystem fileSystem)
        {
            _configurator.AddOption(new ConfigOption<IFileSystem>("filesystem", fileSystem));
        }
        
        private void SetFileWriter()
        {
            _configurator.AddOption(new ConfigOption<IFileWriter>("exporter.filewriter", new FileWriter(GetFileSystem())));
        }
    }
}