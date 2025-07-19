using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation.Rules;

namespace McdaToolkit.Data.Validation.MatrixValidation;

internal sealed class MatrixValidation : ValidationServiceBase
{
    public MatrixValidation(double[,] matrix, double[] weights, int[] criteriaTypes)
    {
        Rules.Add(new IsMcdaInputDataNotNullRule(matrix, weights, criteriaTypes));
        Rules.Add(new IsWeightsEqualOneRule(weights));
        Rules.Add(new IsCriteriaDesisionBetweenMinusOneAndOneRule(criteriaTypes));
        Rules.Add(new IsDataWeightsAndTypesHaveCorrectSizesRule(matrix,weights,criteriaTypes));
    }
}