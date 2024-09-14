using System.Collections.Generic;

namespace McdaToolkit.Configuration
{
    public interface IToolkitConfiguration
    {
        /// <summary>
        /// Get all config options identified by unique key 
        /// </summary>
        /// <returns>Sequence of config options (sequence can be empty)</returns>
        IEnumerable<IConfigOption> GetOptions();
    }
}