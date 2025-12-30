using McdaToolkit.Exporters.Abstraction.Path;

namespace McdaToolkit.Exporters.Abstraction;

public interface IExporterSettings
{
    public OutputPath Path { get; }
}