using System.Text.Json;
using McdaToolkit.Exporters.Abstraction;
using McdaToolkit.Exporters.Abstraction.Path;

namespace McdaToolkit.Exporters.Json;

public class JsonExporterSettings : IExporterSettings
{
    public JsonSerializerOptions JsonSerializerOptions { get; init; } = JsonSerializerOptions.Default;
    public OutputPath Path { get; init; } = OutputPath.Default;
}