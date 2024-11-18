using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Extensions;
using McdaToolkit.Normalization.Enums;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Normalization.Services.Abstraction;
using McdaToolkit.Normalization.Services.MatrixNormalizator.Errors;

namespace McdaToolkit.Normalization.Services.MatrixNormalizator;

public sealed class MatrixNormalizatorService : IMatrixNormalizationService
{
    private IVectorNormalizator<double> _vectorNormalizatorMethod;

    public MatrixNormalizatorService(NormalizationMethod name)
    {
        GetCurrentNormalizationName = name;
        _vectorNormalizatorMethod = NormalizationMethodFactory.Create(GetCurrentNormalizationName);
    }

    /// <inheritdoc cref="IMatrixNormalizator{T}.NormalizeMatrix}"/>
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

    public NormalizationMethod GetCurrentNormalizationName { get; }
    
    public IResult ChangeNormalizationMethod(NormalizationMethod newMethod)
    {
        if (newMethod == GetCurrentNormalizationName)
        {
            return Result.Fail(new NormalizationMethodsEqualError());
        }
        _vectorNormalizatorMethod = NormalizationMethodFactory.Create(newMethod);
        return Result.Ok();
    }
}