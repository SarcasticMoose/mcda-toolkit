using System.Diagnostics.CodeAnalysis;

namespace McdaToolkit.Validation;

/// <summary>Thrown when MCDA input data fails validation.</summary>
[ExcludeFromCodeCoverage]
public class ValidationException : Exception
{
    /// <summary>Initializes the exception with a message describing the validation failure.</summary>
    public ValidationException(string message) : base(message) { }
}
