using System.Collections.Generic;

namespace McdaToolkit.Configuration
{
    public interface IBaseConfiguration
    {
        IEnumerable<IConfigOption> GetOptions();
    }
}