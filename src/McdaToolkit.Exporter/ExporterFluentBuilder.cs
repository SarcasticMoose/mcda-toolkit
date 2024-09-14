using System;
using McdaToolkit.Exporter.Configuration;
using McdaToolkit.FileIO.Path;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter
{
    public sealed class ExporterFluentBuilder : IExporterFluentBuilder
    {
        private readonly ExporterConfigurator _configurator = new ExporterConfigurator();

        public IExporterFluentBuilder WithFormatter(ISerializer serializer)
        {
            _configurator.SetSerializer(serializer);
            return this;
        }
        
        public IExporterFluentBuilder WithPath(Action<IPathFluentBuilder> configure)
        {
            var pathBuilder = new PathFluentBuilder(_configurator.GetConfiguration());
            configure.Invoke(pathBuilder);
            _configurator.SetPath(pathBuilder.Build());
            return this;
        }

        public IExporter Build()
        {
            var path = _configurator.GetPath();
            return new ExporterFactory().Create(_configurator);
        }
    }
}