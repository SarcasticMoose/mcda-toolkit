using LightResults;

namespace McdaToolkit.Serializer.Abstraction
{
    public interface ISerializer
    {
        Result<T> Deserialize<T>(string text);
        Result<string> Serialize<T>(T obj);
        string Extension { get; }
    }
}