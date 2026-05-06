using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Models.Builders;

/// <summary>
/// Builds valid MCDA input data step by step, allowing for incremental setup of the decision matrix,
/// weights, and criteria types. Ensures validation before construction.
/// </summary>
public class DataBuilder<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private T[,]? _matrix;
    private List<CriterionDefinition<T>>? _criteria;

    public static DataBuilder<T> Create() => new();

    private DataBuilder() { }

    /// <summary>
    /// Sets the decision matrix, where each row is an alternative and each column is a criterion.
    /// </summary>
    public DataBuilder<T> AddDecisionMatrix(T[,] matrix)
    {
        _matrix = matrix;
        return this;
    }

    /// <summary>
    /// Adds a criterion definition using a builder action.
    /// </summary>
    public DataBuilder<T> AddCriteria(Action<CriteriaBuilder<T>> action)
    {
        _criteria ??= [];
        var builder = new CriteriaBuilder<T>();
        action(builder);
        _criteria.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Validates the input data and builds an instance of <see cref="McdaProblem{T}"/> if all values are valid.
    /// </summary>
    /// <exception cref="ValidationException">Thrown if validation fails.</exception>
    public McdaProblem<T> Build()
    {
        var result = McdaInputValidation.Validate(_matrix, _criteria);
        if (result.IsFailure())
            throw ValidationExtensions.ToException(result);

        return new McdaProblem<T>
        {
            Data = Matrix<T>.Build.DenseOfArray(_matrix!),
            Criteria = _criteria!.ToArray()
        };
    }
}
