namespace McdaToolkit.Configuration
{
    public interface IConfigOption
    {
        string Key { get; }
        object Value { get; }
    }

    public interface IConfigOption<out T> : IConfigOption
    {
        new string Key { get; }
        new T Value { get; }
    }
}