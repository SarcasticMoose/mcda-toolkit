using LightResults;
using MathNet.Numerics;
using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Data.Validation.MatrixValidation.Rules;

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