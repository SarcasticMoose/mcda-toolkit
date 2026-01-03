using McdaToolkit.Exporters.Abstraction;
using McdaToolkit.Exporters.Abstraction.FileWriter;

namespace McdaToolkit.Exporters.Json;

public sealed class JsonExporterBuilder
{
    private IFileWriter? _fileWriter;
    private JsonExporterSettings? _settings;

    internal JsonExporterBuilder(IFileWriter fileWriter)
    {
        _fileWriter = fileWriter;
    }

    public JsonExporterBuilder WithSettings(JsonExporterSettings settings)
    {
        _settings = settings;
        return this;
    }

    public IExporter Build()
    {
        _fileWriter ??= new InternalFileWriter();
        _settings ??= new JsonExporterSettings();
        return new JsonExporter(_settings, _fileWriter);
    }
}