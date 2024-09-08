using System.Collections.Generic;
using System.Linq;

namespace McdaToolkit.Configuration
{
    public class Configurator : IConfigurator, IReadOnlyConfigurator
    {
        private readonly HashSet<IConfigOption> _options = new HashSet<IConfigOption>();

        public Configurator(IEnumerable<IConfigOption> options)
        {
            AddRange(options);
        }
        
        public Configurator()
        {
        }
        
        public IEnumerable<IConfigOption> GetOptions()
        {
            return _options.AsEnumerable();
        }
        
        public IConfigOption<T>? GetOptionOrDefault<T>(string key)
        {
            return (IConfigOption<T>?)GetOption(key);
        }
        
        public IConfigOption<T> GetOption<T>(string key)
        {
            return (IConfigOption<T>)GetOption(key)!;
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
        
        private IConfigOption? GetOption(string key)
        {
            return _options.FirstOrDefault(x => x.Key == key);
        }
    }
}
