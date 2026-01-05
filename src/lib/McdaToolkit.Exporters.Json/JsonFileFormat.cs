using McdaToolkit.Exporters.Abstraction.FileFormat;

namespace McdaToolkit.Exporters.Json
{
    public class JsonFileFormat : IFileFormat
    {
        public string Format => JsonExporterConstNames.Json;
    }
}