using LightResults;

namespace McdaToolkit.Validation.Abstraction;

/// <summary>Helper methods for converting validation results to exceptions.</summary>
public static class ValidationExtensions
{
    /// <summary>Creates a <see cref="ValidationException"/> from a failed <see cref="IResult"/>.</summary>
    public static ValidationException ToException(IResult result)
        => new($"Failed because of: {string.Join(", ", result.Errors)}");
}