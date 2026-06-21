using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Core.Builders;

/// <summary>
/// Provides extension methods for <see cref="McdaProblemBuilder{T}"/>.
/// </summary>
public static class McdaProblemBuilderExtensions
{
    /// <summary>Sets the decision matrix from a row-major sequence.</summary>
    public static McdaProblemBuilder<T> WithMatrix<T>(
        this McdaProblemBuilder<T> builder,
        IEnumerable<IEnumerable<T>> rows)
        where T : struct, IFloatingPointIeee754<T>
    {
        builder.WithMatrix(Matrix<T>.Build.DenseOfRows(rows));
        return builder;
    }

    /// <summary>Sets the decision matrix.</summary>
    public static McdaProblemBuilder<T> WithMatrix<T>(
        this McdaProblemBuilder<T> builder,
        Matrix<T> matrix)
        where T : struct, IFloatingPointIeee754<T>
    {
        builder.WithMatrix(matrix.ToArray());
        return builder;
    }
}
