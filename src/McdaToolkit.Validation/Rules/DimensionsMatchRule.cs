using System.Numerics;
using LightResults;
using McdaToolkit.Validation.Context;
using McdaToolkit.Validation.Errors;

namespace McdaToolkit.Validation.Rules;

internal sealed class DimensionsMatchRule<T> : IValidationRule<ValidMcdaInput<T>>
    where T : struct, IFloatingPointIeee754<T>
{
    public IResult IsValid(ValidMcdaInput<T> context)
    {
        var cols = context.Matrix.GetLength(1);
        return cols == context.Criteria.Length
            ? Result.Success()
            : Result.Failure(new MatrixColumnLengthNotEqualWeightsVectorLengthError());
    }
}
