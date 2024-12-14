#nullable disable
using LightResults;
using McdaToolkit.Validation.Abstraction;
using McdaToolkit.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Validation.MatrixValidation.Rules;

public class IsDataWeightsAndTypesHaveCorrectSizesRule : IValidationRule
{
    private readonly double[,] _matrix;
    private readonly double[] _weights;
    private readonly int[] _criteriaDecision;

    public IsDataWeightsAndTypesHaveCorrectSizesRule(double[,] matrix,double[] weights,int[] criteriaDecision)
    {
        _matrix = matrix;
        _weights = weights;
        _criteriaDecision = criteriaDecision;
    }
    public IResult IsValid()
    {
        var matrixColumns = _matrix.GetLength(1);
        var isCorrectSizes = matrixColumns == _weights.Length && matrixColumns == _criteriaDecision.Length;
        if (!isCorrectSizes)
        {
            return Result.Fail(new MatrixColumnLengthNotEqualWeightsVectorLengthError());
        }
        return Result.Ok();
    }
}