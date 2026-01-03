using System.Text.Json;
using LightResults;
using McdaToolkit.Data;
using McdaToolkit.Exporters.Abstraction;
using McdaToolkit.Exporters.Abstraction.FileWriter;

namespace McdaToolkit.Exporters.Json;

internal sealed class JsonExporter : IExporter
{
    private readonly JsonExporterSettings _settings;
    private readonly IFileWriter _fileWriter;

    public JsonExporter(
        JsonExporterSettings settings,
        IFileWriter fileWriter)
    {
        _settings = settings;
        _fileWriter = fileWriter;
    }
    
    public async ValueTask<IResult> ExportAsync(
        ExecutionDetails details,
        CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(details);
        await using Stream stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, details, _settings.JsonSerializerOptions, ct);
        return await _fileWriter.WriteToFile(stream, _settings.Path, ct);
    }
}