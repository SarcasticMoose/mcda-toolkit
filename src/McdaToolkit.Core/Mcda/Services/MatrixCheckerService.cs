using LightResults;
using McdaToolkit.Core.Mcda.Helpers;
using McdaToolkit.Core.Mcda.Helpers.Errors;

namespace McdaToolkit.Core.Mcda.Services;

internal sealed class MatrixCheckerService
{
    public Result ValidateData(double[,] matrix, double[] weights, int[] criteriaDirections)
    {
        var isWeightsCorrect = CheckDataHelper.IsWeightEqualOne(weights);
        if (!isWeightsCorrect)
        {
            return Result.Fail(new WeightNotSumToOneError());
        }

        var isCriteriaDecisionCorrect = CheckDataHelper.IsCriteriaDesisionBetweenMinusOneAndOne(criteriaDirections);
        if (!isCriteriaDecisionCorrect)
        {
            return Result.Fail(new DecisionCriteriaHaveIncorrectValueError());
        }

        var isSizesAreCorrect = CheckDataHelper.IsDataWeightsAndTypesHaveCorrectSizes(matrix, weights, criteriaDirections);
        if (!isSizesAreCorrect)
        {
            return Result.Fail(new MatrixColumnLengthNotEqualWeightsVectorLengthError());
        }
        return Result.Ok();
    }
}