using System.Collections.Generic;
using System.Linq;
using LightResults;
using McdaToolkit.Configuration.Comparers;

namespace McdaToolkit.Configuration
{
    public sealed class ToolkitConfiguration : IToolkitConfiguration
    {
        private readonly HashSet<IConfigOption> _options = new HashSet<IConfigOption>(new ConfigOptionKeyComparer());
        
        /// <inheritdoc cref="IToolkitConfiguration.GetOptions"/>
        public IEnumerable<IConfigOption> GetOptions()
        {
            return _options;
        }
        
        /// <summary>
        /// Get an config option represented identified by unique key 
        /// </summary>
        /// <param name="key">Unique identifier to indicate option in configuration</param>
        /// <exception cref="System.InvalidOperationException">No matching element is configuration</exception>
        /// <returns>Configuration option that satisfy provided filtering</returns>
        public IConfigOption GetOption(string key)
        { 
            return _options.First(x => x.Key == key);
        }
        
        /// <summary>
        /// Get a generic config option represented identified by unique key 
        /// </summary>
        /// <param name="key">Unique identifier to indicate option in configuration</param>
        /// <typeparam name="T">Type of the option</typeparam>
        /// <exception cref="System.InvalidOperationException">No matching element is configuration</exception>
        /// <returns>Unique config option that satisfy provided filtering</returns>
        public IConfigOption<T> GetOption<T>(string key)
        {
            return (IConfigOption<T>)_options.First(x => x.Key == key && x.Value is T);
        }
        
        /// <summary>
        /// Get an config option represented identified by unique key
        /// </summary>
        /// <param name="key">Unique identifier to indicate option in configuration</param>
        /// <returns>Unique config option that satisfy provided filtering or default value when there is no matching element</returns>
        public IConfigOption? GetOptionOrDefault(string key)
        {
            return (IConfigOption?)_options.FirstOrDefault(x => x.Key == key);
        }
        
        /// <summary>
        /// Get a generic config option represented identified by unique key 
        /// </summary>
        /// <param name="key">Unique identifier to indicate option in configuration</param>
        /// <typeparam name="T">Type of the option</typeparam>
        /// <returns>Return unique config option that satisfy provided filtering or default value when there is no matching element</returns>
        public IConfigOption<T>? GetOptionOrDefault<T>(string key)
        {
            return (IConfigOption<T>?)_options.FirstOrDefault(x => x.Key == key && x.Value is T);
        }
        
        /// <summary>
        /// Add range of config options identified by unique key 
        /// If the option with the same identifier exists in the configuration, the new one will be omitted.
        /// </summary>
        /// <param name="options">Sequence of the new config options</param>
        /// <returns>Sequence of adding operation results</returns>
        public IEnumerable<Result> AddRange(IEnumerable<IConfigOption> options)
        {
            var results = new Queue<Result>();
            foreach (var option in options)
            {
                results.Enqueue(AddOption(option));
            }
            return results;
        }
        
        /// <summary>
        /// Add new config options identified by unique key 
        /// If the option with the same identifier exists in the configuration, the new one will be omitted.
        /// </summary>
        /// <param name="option">new option</param>
        /// <returns>Result of the adding new config option</returns>
        public Result AddOption(IConfigOption option)
        {
            var addingResult = _options.Add(option);
            return addingResult ? Result.Ok() : Result.Fail();
        }
    }
}