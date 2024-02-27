using MathNet.Numerics.LinearAlgebra;
using McdaMethods.NormalizationMethods;

namespace McdaMethods.Helpers;

public class DataNormalization : IDataNormalization
{
    private readonly INormalizationMethod _method;

    public DataNormalization(
        INormalizationMethod method)
    {
        _method = method;
    }

    public Matrix<double> NormalizeMatrix(Matrix<double> matrix, int[] criteriaTypes)
    {
        var normalizedMatrix = Matrix<double>.Build.Dense(matrix.RowCount, matrix.ColumnCount);
        matrix.CopyTo(normalizedMatrix);

        for (int i = 0; i < matrix.ColumnCount; i++)
        {
            var rowVector = matrix.Column(i);

            if (criteriaTypes[i] == 1)
            {
                normalizedMatrix.SetColumn(i, _method.Normalize<double>(rowVector, false));
            }
            else
            {
                normalizedMatrix.SetColumn(i, _method.Normalize<double>(rowVector, true));
            }
        }
        return normalizedMatrix;
    }
}