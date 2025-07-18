#nullable disable
using LightResults;
using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation.Errors;

namespace McdaToolkit.Data.Validation.MatrixValidation.Rules;

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
            return Result.Failure(new MatrixColumnLengthNotEqualWeightsVectorLengthError());
        }
        return Result.Success();
    }
}