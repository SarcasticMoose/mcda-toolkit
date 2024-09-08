using McdaToolkit.Exporter.Configuration;

namespace McdaToolkit.Exporter
{
    internal class ExporterFactory
    {
        public IExporter Create(IExporterConfiguration configuration)
        {
            var serializer = configuration.GetSerializer();
            var fileWriter = configuration.GetFileWriter();
            var path = configuration.GetPath();
            return new DefaultExporter(serializer,fileWriter, path);
        }
    }
}