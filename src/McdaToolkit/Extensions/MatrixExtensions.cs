using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Extensions;

public static class MatrixExtensions
{
    public static Vector<T> GetColMax<T>(this Matrix<T> matrix) where T : struct, IEquatable<T>, IFormattable
    {
        var maxVector = Vector<T>.Build.Dense(matrix.ColumnCount);

        for (int i = 0; i < matrix.ColumnCount; i++)
        {
            maxVector[i] = matrix.Column(i).Maximum();
        }
        return maxVector;
    }
    
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