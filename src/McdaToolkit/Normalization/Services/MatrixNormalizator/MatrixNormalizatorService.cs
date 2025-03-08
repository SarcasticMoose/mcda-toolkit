using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Enums;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Normalization.Services.Abstraction;

namespace McdaToolkit.Normalization.Services.MatrixNormalizator;

internal sealed class MatrixNormalizatorService : IMatrixNormalizationService
{
    private readonly IVectorNormalizator<double> _vectorNormalizatorMethod;

    public MatrixNormalizatorService(IVectorNormalizator<double> vectorNormalizatorMethod)
    {
        _vectorNormalizatorMethod = vectorNormalizatorMethod;
    }

    /// <inheritdoc cref="IMatrixNormalizationService.NormalizeMatrix"/>
    public Matrix<double> NormalizeMatrix(Matrix<double> matrix, int[] criteriaTypes)
    {
        for (int colIndex = 0; colIndex < matrix.ColumnCount; colIndex++)
        {
            matrix.SetColumn(columnIndex: colIndex,
                column: criteriaTypes[colIndex] == 1
                    ? _vectorNormalizatorMethod.Normalize(data: matrix.Column(colIndex), cost: false)
                    : _vectorNormalizatorMethod.Normalize(data: matrix.Column(colIndex), cost: true));
        }
        return matrix;
    }
}