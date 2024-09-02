namespace McdaToolkit.Configuration
{
    public interface IBuilder<out T> 
    {
        T Build();
    }
}