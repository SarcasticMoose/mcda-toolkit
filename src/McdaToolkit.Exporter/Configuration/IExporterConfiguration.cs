using McdaToolkit.Configuration;
using McdaToolkit.FileIO.Path;
using McdaToolkit.FileIO.Writer;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter.Configuration
{
    public interface IExporterConfiguration
    {
        IBaseConfiguration GetConfiguration();
        IToolkitPath GetPath();
        ISerializer GetSerializer();
        IFileWriter GetFileWriter();
    }
}