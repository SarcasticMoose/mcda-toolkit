using System.Collections.Generic;
using System.Linq;

namespace McdaToolkit.Configuration
{
    public class Configurator : IConfigurator
    {
        private readonly HashSet<IConfigOption> _options = new HashSet<IConfigOption>();
    
        public IEnumerable<IConfigOption> GetOptions()
        {
            return _options.AsEnumerable();
        }

        public IConfigOption? GetOption(string key)
        {
            return _options.FirstOrDefault(x => x.Key == key);
        }

        public IConfigOption<T>? GetOption<T>(string key)
        {
            return (IConfigOption<T>?)_options.FirstOrDefault(x => x.Key == key && x.Value is T);
        }

        public void AddOption(IConfigOption option)
        {
            _options.Add(option);
        }

        public void AddRange(IEnumerable<IConfigOption> options)
        {
            foreach (var option in options)
            {
                AddOption(option);
            }
        }
    }
}