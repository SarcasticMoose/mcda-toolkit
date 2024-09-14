using McdaToolkit.Exporter.Configuration;

namespace McdaToolkit.Exporter
{
    internal class ExporterFactory
    {
        public IExporter Create(IExporterConfigurator configurator)
        {
            var serializer = configurator.GetSerializer();
            var fileWriter = configurator.GetFileWriter();
            var path = configurator.GetPath();
            return new DefaultExporter(serializer,fileWriter, path);
        }
    }
}