using System.Numerics;
using LightResults;
using McdaToolkit.Validation.Abstraction;
using McdaToolkit.Validation.MatrixValidation.Context;
using McdaToolkit.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Validation.MatrixValidation.Rules;

internal sealed class NotNullRule<T> : IValidationRule<RawMcdaInput<T>>
    where T : struct, IFloatingPointIeee754<T>
{
    public IResult IsValid(RawMcdaInput<T> context)
    {
        ICollection<IError> errors = [];
        if (context.Matrix   is null) errors.Add(new NullMatrixDataError());
        if (context.Criteria is null) errors.Add(new NullCriteriaTypesDataError());
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
    }
}
