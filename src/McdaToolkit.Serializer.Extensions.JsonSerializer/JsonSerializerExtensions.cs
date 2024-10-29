using System.Text.Json;
using McdaToolkit.Exporter;

namespace McdaToolkit.Serializer.Extensions.JsonSerializer;

public static class JsonSerializerExtensions
{
    public static IExporterFluentBuilder WithJsonSerializerFormatter(this IExporterFluentBuilder exporterFluentBuilder, JsonSerializerOptions? options = null)
    {
        return exporterFluentBuilder.WithFormatter(new JsonFormatter(options));
    }
}