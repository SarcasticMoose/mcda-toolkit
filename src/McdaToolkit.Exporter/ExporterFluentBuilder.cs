using System;
using McdaToolkit.Exporter.Configuration;
using McdaToolkit.FileIO.Path;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter
{
    public sealed class ExporterFluentBuilder : IExporterFluentBuilder
    {
        private readonly ExporterConfiguration _configuration = new ExporterConfiguration();

        public IExporterFluentBuilder WithFormatter(ISerializer serializer)
        {
            _configuration.SetSerializer(serializer);
            return this;
        }
        
        public IExporterFluentBuilder WithPath(Action<IPathFluentBuilder> configure)
        {
            var pathBuilder = new PathFluentBuilder(_configuration.GetConfiguration());
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