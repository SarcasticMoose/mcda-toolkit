using System.IO.Abstractions;
using McdaToolkit.Configuration;
using McdaToolkit.FileIO.Configuration;
using McdaToolkit.FileIO.Path;
using McdaToolkit.FileIO.Path.Factories;
using McdaToolkit.FileIO.Writer;
using McdaToolkit.Serializer;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter.Configuration
{
    internal class ExporterConfigurator : IExporterConfigurator
    {
        private readonly ToolkitConfiguration _toolkitConfiguration = new ToolkitConfiguration();

        public ExporterConfigurator()
        {
            SetFileWriter();
        }

        public IToolkitConfiguration GetConfiguration() => _toolkitConfiguration;
        
        public IToolkitPath GetPath()
        {
            var pathConfig = _toolkitConfiguration.GetOptionOrDefault<IToolkitPath>(ExporterConfigKeys.ExporterOutputPath);
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
            _toolkitConfiguration.AddOption(new ConfigOption<IToolkitPath>(ExporterConfigKeys.ExporterOutputPath, path));
        }
        
        public ISerializer GetSerializer()
        {
            var serializerConfig = _toolkitConfiguration.GetOptionOrDefault<ISerializer>(ExporterConfigKeys.ExporterSerializer);
            if (serializerConfig != null)
            {
                return serializerConfig.Value;
            }
            var serializer = new DefaultXmlSerializer();
            SetSerializer(serializer);
            return serializer;
        }
        
        public void SetSerializer(ISerializer serializer)
        {
            _toolkitConfiguration.AddOption(new ConfigOption<ISerializer>(ExporterConfigKeys.ExporterSerializer, serializer));
        }

        public IFileSystem GetFileSystem()
        {
            var fileSystemConfig = _toolkitConfiguration.GetOptionOrDefault<IFileSystem>(FileIoConfigKeys.FileSystem);
            if (fileSystemConfig is null)
            {
                SetFileSystem(new FileSystem());
            }
            return _toolkitConfiguration.GetOption<IFileSystem>(FileIoConfigKeys.FileSystem).Value;
        }

        public IFileWriter GetFileWriter()
        {
            return _toolkitConfiguration.GetOption<IFileWriter>(ExporterConfigKeys.ExporterWriter).Value;
        }

        private void SetFileSystem(IFileSystem fileSystem)
        {
            _toolkitConfiguration.AddOption(new ConfigOption<IFileSystem>(FileIoConfigKeys.FileSystem, fileSystem));
        }
        
        private void SetFileWriter()
        {
            _toolkitConfiguration.AddOption(new ConfigOption<IFileWriter>(ExporterConfigKeys.ExporterWriter, new FileWriter(GetFileSystem())));
        }
    }
}