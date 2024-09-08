using System;
using McdaToolkit.Builder.Abstraction;
using McdaToolkit.FileIO.Path;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter
{
    public interface IExporterBuilder : IBuilder<IExporter>
    {
        IExporterBuilder WithPath(Action<IPathBuilder> configure);
        IExporterBuilder WithFormatter(ISerializer serializer);
    }
}