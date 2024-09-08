using System;
using McdaToolkit.Exporter.Configuration;
using McdaToolkit.FileIO.Path;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter
{
    public sealed class ExporterBuilder : IExporterBuilder
    {
        private readonly ExporterConfiguration _configuration = new ExporterConfiguration();

        public IExporterBuilder WithFormatter(ISerializer serializer)
        {
            _configuration.SetSerializer(serializer);
            return this;
        }
        
        public IExporterBuilder WithPath(Action<IPathBuilder> configure)
        {
            var pathBuilder = new PathBuilder(_configuration.GetConfiguration());
            configure.Invoke(pathBuilder);
            _configuration.SetPath(pathBuilder.Build());
            return this;
        }

        public IExporter Build()
        {
            var path = _configuration.GetPath();
            return new ExporterFactory().Create(_configuration);
        }
    }
}