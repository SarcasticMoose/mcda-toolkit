namespace McdaToolkit.Configuration
{
    public class ConfigOption<T> : IConfigOption<T>
    {
        public string Key { get; }
        public T Value { get; }

        public ConfigOption(string key, T value)
        {
            Key = key;
            Value = value;
        }
    
        object IConfigOption.Value => Value!;
    }
}