namespace McdaToolkit.Builder.Abstraction
{
    /// <summary>
    /// Interfaces indicate fluent builder
    /// </summary>
    public interface IFluentBuilder<out T>
    {
        /// <summary>
        /// Build type created via builder
        /// </summary>
        /// <returns>Created type</returns>
        T Build();
    }
}