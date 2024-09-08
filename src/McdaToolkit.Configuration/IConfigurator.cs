using System.Collections.Generic;

namespace McdaToolkit.Configuration
{
    public interface IConfigurator
    {
        void AddOption(IConfigOption option);
        void AddRange(IEnumerable<IConfigOption> option);
    }
}