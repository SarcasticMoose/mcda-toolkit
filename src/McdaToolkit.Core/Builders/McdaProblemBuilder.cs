using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Core.Builders;

/// <summary>Fluent builder for creating an <see cref="McdaProblem{T}"/>.</summary>
public class McdaProblemBuilder<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private Matrix<T>? _matrix;
    private readonly List<CriteriaBuilder<T>> _criteria = [];

    internal McdaProblemBuilder() {}

    /// <summary>Sets the decision matrix from a 2D array (rows × columns).</summary>
    public McdaProblemBuilder<T> WithMatrix(T[,] matrix)
    {
        _matrix = Matrix<T>.Build.DenseOfArray(matrix);
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

    internal McdaProblem<T> Build() => McdaProblem<T>.Create(_matrix ?? Matrix<T>.Build.DenseOfArray(new T[0, 0]), [.. _criteria.Select(c => c.Build())]);
}
