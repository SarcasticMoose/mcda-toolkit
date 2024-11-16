using System;
using McdaToolkit.Fluent.Core;
using McdaToolkit.FileIO.Path;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter
{
    public interface IExporterFluentBuilder : IFluentBuilder<IExporter>
    {
        IExporterFluentBuilder WithPath(Action<IPathFluentBuilder> configure);
        IExporterFluentBuilder WithFormatter(ISerializer serializer);
    }
}