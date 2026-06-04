using LightResults;

namespace McdaToolkit.Validation.Rules;

internal interface IValidationRule<in T>
{
    IResult IsValid(T context);
}
