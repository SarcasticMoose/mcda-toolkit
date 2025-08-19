using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data.Operations.Normalization.Methods.Abstraction;
using McdaToolkit.Data.Operations.Normalization.Service.MatrixNormalizator;
using McdaToolkit.Data.Operations.Weighting;

namespace McdaToolkit.Data.Operations;

internal sealed class MatrixOperations : IMatrixNormalizator, IMatrixWeighter
{
    private readonly IVectorNormalizator<double> _vectorNormalizatorMethod;

    public MatrixOperations(
        IVectorNormalizator<double> vectorNormalizatorMethod)
    {
        _vectorNormalizatorMethod = vectorNormalizatorMethod;
    }

    /// <inheritdoc cref="IMatrixNormalizator.Normalize"/>
    public Matrix<double> Normalize(Matrix<double> matrix, int[] criteriaTypes)
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

    /// <inheritdoc cref="IMatrixWeighter.Weight"/>
    public Matrix<double> Weight(Matrix<double> matrix, Vector<double> weights)
    {
        for (int i = 0; i < matrix.RowCount; i++)
        {
            matrix.SetRow(i, matrix.Row(i).PointwiseMultiply(weights));
        }
        return matrix;
    }
}