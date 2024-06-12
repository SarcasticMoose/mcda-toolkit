using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.McdaMethods.Errors;
using McdaToolkit.McdaMethods.Helpers;
using McdaToolkit.McdaMethods.Interfaces;

namespace McdaToolkit.McdaMethods.Abstraction;

public abstract class McdaMethod : IMethod
{
    private Result InitialErrorsCheck(double[,] matrix, double[] weights, int[] criteriaDirections)
    {
        if (weights == null)
        {
            throw new ArgumentNullException(nameof(weights), "Value cannot be null");
        }
        
        if (criteriaDirections == null)
        {
            throw new ArgumentNullException(nameof(criteriaDirections), "Value cannot be null");
        }
        
        var isWeightsCorrect = CheckDataHelper.IsWeightEqualOne(weights);

        if (!isWeightsCorrect)
        {
            return Result.Fail(new WeightNotSumToOneError());
        }

        var isCriteriaDecisionCorrect = CheckDataHelper.IsCriteriaDesisionBetweenMinusOneAndOne(criteriaDirections);

        if (!isCriteriaDecisionCorrect)
        {
            return Result.Fail(new CriteriaNotBetweenMinusOneAndOne());
        }

        var isSizesAreCorrect = CheckDataHelper.IsDataWeightsAndTypesHaveCorrectSizes(matrix, weights, criteriaDirections);

        if (!isSizesAreCorrect)
        {
            return Result.Fail(new ArraySizesAreNotEqual());
        }

        return Result.Ok();
    } 
    public Result<Vector<double>> Calculate(double[,] matrix, double[] weights, int[] criteriaDirections)
    {
        var errorsCheckResult = InitialErrorsCheck(matrix,weights, criteriaDirections);

        if (errorsCheckResult.IsFailed)
        {
            return Result.Fail<Vector<double>>(errorsCheckResult.Errors);
        }
        
        var matrixTypeOfMatrix = Matrix<double>.Build.DenseOfArray(matrix);
        return Calculate(matrixTypeOfMatrix, weights, criteriaDirections);
    }
    protected abstract Result<Vector<double>> Calculate(Matrix<double> matrix, double[] weights, int[] criteriaDirections);
}