using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Core;

/// <summary>
/// Provides extension methods for <see cref="Matrix{T}"/>.
/// </summary>
public static class MatrixExtensions
{
    /// <summary>
    /// Returns the maximum value in each column of the matrix.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="matrix">The matrix to process.</param>
    /// <returns>A vector containing the maximum value in each column.</returns>
    public static Vector<T> GetColMax<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable
    {
        var maxVector = Vector<T>.Build.Dense(matrix.ColumnCount);

        for (int i = 0; i < matrix.ColumnCount; i++)
        {
            maxVector[i] = matrix.Column(i).Maximum();
        }
        return maxVector;
    }

    /// <summary>
    /// Returns the minimum value in each column of the matrix.
    /// </summary>
    /// <typeparam name="T">The type of the matrix elements.</typeparam>
    /// <param name="matrix">The matrix to process.</param>
    /// <returns>A vector containing the minimum value in each column.</returns>
    public static Vector<T> GetColMin<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable
    {
        var maxVector = Vector<T>.Build.Dense(matrix.ColumnCount);

        for (int i = 0; i < matrix.ColumnCount; i++)
        {
            maxVector[i] = matrix.Column(i).Min();
        }
        return maxVector;
    }
}
