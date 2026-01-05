using LightResults;

namespace McdaToolkit.Data.Validation.Abstraction;

public static class ValidationExtensions
{
    public static ValidationException ToException(IResult result)
        => new($"Failed because of: {string.Join(", ", result.Errors)}");
}