using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaMethods.Normalization.NormalizationMethods.Abstraction;

namespace McdaMethods.Normalization;

public class DataNormalization<TValue>(INormalizationMethod<TValue> method) : IDataNormalization<TValue>
where TValue : struct, INumber<TValue>
{
    public Matrix<TValue> NormalizeMatrix(Matrix<TValue> matrix, int[] criteriaTypes)
    {
        var normalizedMatrix = Matrix<TValue>.Build.Dense(matrix.RowCount, matrix.ColumnCount);
        matrix.CopyTo(normalizedMatrix);

        for (var i = 0; i < matrix.ColumnCount; i++)
        {
            var rowVector = matrix.Column(i);

            if (criteriaTypes[i] == 1)
            {
                normalizedMatrix.SetColumn(i, method.Normalize<TValue>(rowVector, false));
            }
            else
            {
                normalizedMatrix.SetColumn(i, method.Normalize<TValue>(rowVector, true));
            }
        }
        return normalizedMatrix;
    }
}