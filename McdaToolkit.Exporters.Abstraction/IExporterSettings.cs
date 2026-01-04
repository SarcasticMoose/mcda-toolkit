using McdaToolkit.Exporters.Abstraction.Path;

namespace McdaToolkit.Exporters.Abstraction
{
    internal interface IExporterSettings
    {
        public OutputPath Path { get; }
    }
}