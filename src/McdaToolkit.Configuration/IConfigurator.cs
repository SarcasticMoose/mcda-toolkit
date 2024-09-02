using System.Collections.Generic;

namespace McdaToolkit.Configuration
{
    public interface IConfigurator
    {
        IEnumerable<IConfigOption> GetOptions();
        IConfigOption? GetOption(string key);
        IConfigOption<T>? GetOption<T>(string key);
        void AddOption(IConfigOption option);
        void AddRange(IEnumerable<IConfigOption> option);
    }
}