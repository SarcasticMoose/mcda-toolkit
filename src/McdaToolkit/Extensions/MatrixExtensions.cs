using System.Runtime.CompilerServices;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Extensions;

/// <summary>Extension methods for <see cref="Matrix{T}"/>.</summary>
public static class MatrixExtensions
{
    /// <summary>Returns a vector of per-column maximum values.</summary>
    public static Vector<T> GetColMax<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable
    {
        var maxVector = Vector<T>.Build.Dense(matrix.ColumnCount);

        for (int i = 0; i < matrix.ColumnCount; i++)
        {
            maxVector[i] = matrix.Column(i).Maximum();
        }
        return maxVector;
    }
    
    /// <summary>Returns a vector of per-column minimum values.</summary>
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