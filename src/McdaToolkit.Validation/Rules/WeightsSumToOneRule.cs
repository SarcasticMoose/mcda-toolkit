using System.Numerics;
using LightResults;
using MathNet.Numerics;
using McdaToolkit.Validation.Abstraction;
using McdaToolkit.Validation.MatrixValidation.Context;
using McdaToolkit.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Validation.MatrixValidation.Rules;

internal sealed class WeightsSumToOneRule<T> : IValidationRule<ValidMcdaInput<T>>
    where T : struct, IFloatingPointIeee754<T>
{
    public IResult IsValid(ValidMcdaInput<T> context)
    {
        var sum = context.Criteria.Aggregate(T.Zero, (acc, c) => acc + c.Weight);
        return double.CreateChecked(sum).AlmostEqual(1)
            ? Result.Success()
            : Result.Failure(new WeightNotSumToOneError());
    }
}
