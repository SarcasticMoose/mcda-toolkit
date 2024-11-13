using LightResults;
using MathNet.Numerics;
using McdaToolkit.Mcda.Services.MatrixChecker.Errors;

namespace McdaToolkit.Mcda.Services.MatrixChecker;

internal sealed class MatrixCheckerService
{
    public Result ValidateData(double[,] matrix, double[] weights, int[] criteriaDirections)
    {
        var isWeightsCorrect = IsWeightEqualOne(weights);
        if (!isWeightsCorrect)
        {
            return Result.Fail(new WeightNotSumToOneError());
        }

        var isCriteriaDecisionCorrect = IsCriteriaDesisionBetweenMinusOneAndOne(criteriaDirections);
        if (!isCriteriaDecisionCorrect)
        {
            return Result.Fail(new DecisionCriteriaHaveIncorrectValueError());
        }

        var isSizesAreCorrect = IsDataWeightsAndTypesHaveCorrectSizes(matrix, weights, criteriaDirections);
        if (!isSizesAreCorrect)
        {
            return Result.Fail(new MatrixColumnLengthNotEqualWeightsVectorLengthError());
        }
        return Result.Ok();
    }
    
    private bool IsWeightEqualOne(double[] weights)
    {
        return weights.Sum().AlmostEqual(1);
    }
    
    private bool IsCriteriaDesisionBetweenMinusOneAndOne(int[] criteriaDecision)
    {
        return criteriaDecision.All(n => n is >= -1 and <= 1);
    }
    
    private bool IsDataWeightsAndTypesHaveCorrectSizes(double[,] matrix,double[] weights,int[] criteriaDecision)
    {
        var matrixColumns = matrix.GetLength(1);
        return matrixColumns == weights.Length && matrixColumns == criteriaDecision.Length;
    }
}