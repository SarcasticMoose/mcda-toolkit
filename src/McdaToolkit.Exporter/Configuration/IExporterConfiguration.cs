using McdaToolkit.Configuration;
using McdaToolkit.FileIO.Path;
using McdaToolkit.FileIO.Writer;
using McdaToolkit.Serializer.Abstraction;

namespace McdaToolkit.Exporter.Configuration
{
    public interface IExporterConfigurator : IConfigurator
    {
        IToolkitPath GetPath();
        ISerializer GetSerializer();
        IFileWriter GetFileWriter();
    }
}