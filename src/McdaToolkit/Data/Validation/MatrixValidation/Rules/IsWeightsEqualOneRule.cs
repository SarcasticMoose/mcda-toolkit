using LightResults;
using MathNet.Numerics;
<<<<<<<< HEAD:src/McdaToolkit/Data/Validation/MatrixValidation/Rules/IsWeightsEqualOneRule.cs
using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Data.Validation.MatrixValidation.Rules;
========
using McdaToolkit.Shared.Validation.Abstraction;
using McdaToolkit.Shared.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Shared.Validation.MatrixValidation.Rules;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Shared/Validation/MatrixValidation/Rules/IsWeightsEqualOneRule.cs

public class IsWeightsEqualOneRule : IValidationRule
{
    private readonly double[] _weights;

    public IsWeightsEqualOneRule(double[] weights)
    {
        _weights = weights;
    }
    
    public IResult IsValid()
    {
        var isWeightsCorrect = _weights.Sum().AlmostEqual(1);
        if (!isWeightsCorrect)
        {
            return Result.Failure(new WeightNotSumToOneError());
        }

        return Result.Success();
    }
}