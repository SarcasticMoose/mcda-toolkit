using LightResults;

namespace McdaToolkit.Validation.Abstraction;

public interface IValidationRule
{
    IResult IsValid();
}