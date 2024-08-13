using LightResults;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using McdaToolkit.Extensions;
using McdaToolkit.Mcda.Contracts;
using McdaToolkit.Mcda.Errors;
using McdaToolkit.Mcda.Helpers;

namespace McdaToolkit.Mcda.Abstraction;

public abstract class McdaMethod : IMcdaMethod
{
    #region Private methods
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
    private Result CheckMatrix(double[,] matrix, double[] weights, int[] criteriaDirections)
    {
        var errorsCheckResult = InitialErrorsCheck(matrix,weights, criteriaDirections);

        return errorsCheckResult.IsFailed ? errorsCheckResult : Result.Ok();
    }
    private MatrixDto PrepareMatrix(IEnumerable<IEnumerable<double>> matrix, IEnumerable<double> weights, IEnumerable<int> criteriaDirections)
    {
        var matrix2D = matrix.To2DArray();
        var weightsArray = weights.ToArray();
        var criteriaDirectionsArray = criteriaDirections.ToArray();
        
        return new(matrix2D, weightsArray, criteriaDirectionsArray);
    }
    #endregion

    #region Protected methods
    protected abstract Result<Vector<double>> RunCalculation(Matrix<double> matrix, Vector<double> weights, int[] criteriaDirections);
    #endregion

    #region Public methods
    /// <inheritdoc cref="ICalculation.Calculate"/>
    public Result<Vector<double>> Calculate(IEnumerable<IEnumerable<double>> matrix, IEnumerable<double> weights, IEnumerable<int> criteriaDirections)
    {
        var matrixDto = PrepareMatrix(matrix, weights, criteriaDirections);
        return Calculate(matrixDto.Matrix, matrixDto.Weights, matrixDto.CriteriaDecision);
    }
    /// <inheritdoc cref="ICalculation.Calculate"/>
    public Result<Vector<double>> Calculate(double[,] matrix, double[] weights, int[] criteriaDirections)
    {
        var checkMatrixResult = CheckMatrix(matrix,weights,criteriaDirections);
        if (checkMatrixResult.IsFailed)
        {
            return Result<Vector<double>>.Fail(new Error());
        }
        
        var finalMatrix = Matrix.Build.DenseOfArray(matrix);
        var weightsVector = Vector.Build.DenseOfArray(weights);
        return RunCalculation(finalMatrix,weightsVector,criteriaDirections);
    }
    #endregion
}