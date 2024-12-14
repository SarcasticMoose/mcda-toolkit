using LightResults;
using MathNet.Numerics;
using McdaToolkit.Validation.Abstraction;
using McdaToolkit.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Validation.MatrixValidation.Rules;

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
            return Result.Fail(new WeightNotSumToOneError());
        }

        return Result.Ok();
    }
}