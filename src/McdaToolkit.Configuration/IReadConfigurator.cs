namespace McdaToolkit.Configuration
{
    public interface IReadConfigurator
    {
        IConfigOption<T>? GetOptionOrDefault<T>(string key);
        IConfigOption<T> GetOption<T>(string key);
    }
}