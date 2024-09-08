using System.Collections.Generic;

namespace McdaToolkit.Configuration
{
    public interface IReadOnlyConfigurator
    {
        IEnumerable<IConfigOption> GetOptions();
        IConfigOption<T>? GetOptionOrDefault<T>(string key);
        IConfigOption<T> GetOption<T>(string key);
    }
}