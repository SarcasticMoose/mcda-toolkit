using LightResults;

namespace McdaToolkit.Shared.Validation.Abstraction;

public interface IValidationRule
{
    IResult IsValid();
}