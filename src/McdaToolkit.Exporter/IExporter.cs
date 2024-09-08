using LightResults;

namespace McdaToolkit.Exporter
{
    public interface IExporter
    {
        Result Export<T>(T data);
    }
}