using LightResults;

namespace McdaToolkit.Validation.Abstraction;

internal interface IValidationRule<in T>
{
    IResult IsValid(T context);
}
