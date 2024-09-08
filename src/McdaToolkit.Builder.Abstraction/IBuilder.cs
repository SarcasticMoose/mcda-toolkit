namespace McdaToolkit.Builder.Abstraction
{
    public interface IBuilder<out T> 
    {
        T Build();
    }
}