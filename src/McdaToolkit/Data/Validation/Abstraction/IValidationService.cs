using LightResults;

namespace McdaToolkit.Data.Validation.Abstraction;

/// <summary>
/// Defines a service responsible for executing validation logic.
/// </summary>
internal interface IValidationService
{
    /// <summary>
    /// Executes the validation process until all rules passed or first failed.
    /// </summary>
    /// <returns>
    /// An <see cref="IResult"/> object containing the outcome of the validation,
    /// including any error messages if validation fails.
    /// </returns>
    IResult Validate();
}