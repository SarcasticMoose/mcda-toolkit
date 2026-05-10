using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Builders;

/// <summary>Fluent builder for creating an <see cref="McdaProblem{T}"/>.</summary>
public class McdaProblemBuilder<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private Matrix<T>? _matrix;
    private readonly List<CriteriaBuilder<T>> _criteria = new();

    /// <summary>Sets the decision matrix.</summary>
    public McdaProblemBuilder<T> WithMatrix(Matrix<T> matrix)
    {
        _matrix = matrix;
        return this;
    }

    /// <summary>Sets the decision matrix from a 2D array (rows × columns).</summary>
    public McdaProblemBuilder<T> WithMatrix(T[,] matrix)
    {
        _matrix = Matrix<T>.Build.DenseOfArray(matrix);
        return this;
    }

    /// <summary>Sets the decision matrix from a row-major sequence.</summary>
    public McdaProblemBuilder<T> WithMatrix(IEnumerable<IEnumerable<T>> rows)
    {
        _matrix = Matrix<T>.Build.DenseOfRows(rows);
        return this;
    }

    /// <summary>Adds a criterion definition.</summary>
    public McdaProblemBuilder<T> AddCriterion(Action<CriteriaBuilder<T>> configure)
    {
        var builder = new CriteriaBuilder<T>();
        configure(builder);
        _criteria.Add(builder);
        return this;
    }

    internal McdaProblem<T> Build() => new()
    {
        Data = _matrix,
        Criteria = [.._criteria.Select(c => c.Build())]
    };
}
