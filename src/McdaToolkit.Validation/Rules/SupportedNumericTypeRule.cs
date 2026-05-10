using System.Numerics;
using LightResults;
using McdaToolkit.Validation.Context;
using McdaToolkit.Validation.Errors;

namespace McdaToolkit.Validation.Rules;

internal sealed class SupportedNumericTypeRule<T> : IValidationRule<RawMcdaInput<T>>
    where T : struct, IFloatingPointIeee754<T>
{
    private static readonly HashSet<Type> SupportedTypes = [typeof(double), typeof(float)];

    public IResult IsValid(RawMcdaInput<T> context) =>
        SupportedTypes.Contains(typeof(T))
            ? Result.Success()
            : Result.Failure(new UnsupportedNumericTypeError(typeof(T).Name));
}
