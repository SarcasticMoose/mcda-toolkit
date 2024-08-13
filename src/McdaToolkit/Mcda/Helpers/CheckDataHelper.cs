using MathNet.Numerics;

namespace McdaToolkit.Mcda.Helpers;

public static class CheckDataHelper
{
    public static bool IsWeightEqualOne(double[] weights)
    {
        return weights.Sum().AlmostEqual(1);
    }
    public static bool IsCriteriaDesisionBetweenMinusOneAndOne(int[] criteriaDecision)
    {
        return criteriaDecision.All(n => n is >= -1 and <= 1);
    }
    public static bool IsDataWeightsAndTypesHaveCorrectSizes(double[,] matrix,double[] weights,int[] criteriaDecision)
    {
        var matrixColumns = matrix.GetLength(1);
        return matrixColumns == weights.Length && matrixColumns == criteriaDecision.Length;
    }
}