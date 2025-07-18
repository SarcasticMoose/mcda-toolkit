using MathNet.Numerics.LinearAlgebra.Double;
using McdaToolkit.Data.Validation.Abstraction;
using McdaToolkit.Data.Validation.MatrixValidation;

namespace McdaToolkit.Data.Builders;

/// <summary>
/// Builds valid MCDA input data step by step, allowing for incremental setup of the decision matrix,
/// weights, and criteria types. Ensures validation before construction.
/// </summary>
public class DataBuilder
{
    private double[,] _matrix;
    private double[] _weigths;
    private int[] _criteriaDecision;

    /// <summary>
    /// Sets the decision matrix, where each row is an alternative and each column is a criterion.
    /// </summary>
    /// <param name="matrix">2D array of values representing the decision matrix.</param>
    /// <returns>The current instance of <see cref="DataBuilder"/> for method chaining.</returns>
    public DataBuilder AddDecisionMatrix(double[,] matrix)
    {
        _matrix = matrix;
        return this;
    }

    /// <summary>
    /// Sets the weights vector representing the importance of each criterion.
    /// </summary>
    /// <param name="weights">An array of weights for the criteria.</param>
    /// <returns>The current instance of <see cref="DataBuilder"/> for method chaining.</returns>
    public DataBuilder AddWeights(double[] weights)
    {
        _weigths = weights;
        return this;
    }

    /// <summary>
    /// Sets the decision criteria types. Use +1 for benefit criteria and -1 for cost criteria.
    /// </summary>
    /// <param name="criteriaDecision">An array defining each criterion's type.</param>
    /// <returns>The current instance of <see cref="DataBuilder"/> for method chaining.</returns>
    public DataBuilder AddDecisionCriteria(int[] criteriaDecision)
    {
        _criteriaDecision = criteriaDecision;
        return this;
    }

    /// <summary>
    /// Validates the input data and builds an instance of <see cref="McdaInputData"/> if all values are valid.
    /// </summary>
    /// <returns>A validated <see cref="McdaInputData"/> object.</returns>
    /// <exception cref="ValidationException">Thrown if validation fails</exception>
    public McdaInputData Build()
    {
        var result = new MatrixValidation(_matrix, _weigths, _criteriaDecision).Validate();
        if (result.IsFailure())
        {
            throw ValidationExtensions.ToException(result);
        }

        return new(
            Matrix.Build.DenseOfArray(_matrix),
            Vector.Build.DenseOfArray(_weigths),
            _criteriaDecision);
    }
}
