using LightResults;

namespace McdaToolkit.Data.Validation.Abstraction;

/// <summary>
/// Represents a validation rule that can be used to verify the correctness of data.
/// </summary>
internal interface IValidationRule
{
    /// <summary>
    /// Checks whether the data satisfies the validation rule.
    /// </summary>
    /// <returns>
    /// An <see cref="IResult"/> object containing the result of the validation.
    /// It includes a success status of validation
    /// </returns>
    IResult IsValid();
}