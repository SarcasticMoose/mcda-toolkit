using McdaToolkit.Shared.Validation.Abstraction;
using McdaToolkit.Shared.Validation.MatrixValidation.Rules;

namespace McdaToolkit.Shared.Validation.MatrixValidation;

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