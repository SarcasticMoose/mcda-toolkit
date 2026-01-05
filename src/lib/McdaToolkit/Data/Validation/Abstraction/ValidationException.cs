namespace McdaToolkit.Data.Validation.Abstraction;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}