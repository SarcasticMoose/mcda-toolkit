using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LightResults;
using McdaToolkit.Data;
using McdaToolkit.Exporters.Abstraction;
using McdaToolkit.Exporters.Abstraction.FileWriter;

namespace McdaToolkit.Exporters.Json
{
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
            if(details is null) throw new ArgumentNullException(nameof(details));
            await using Stream stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, details, _settings.JsonSerializerOptions, ct);
            return await _fileWriter.WriteToFile(stream, _settings.Path, ct);
        }
    }
}