using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Core.Enums;
using McdaToolkit.Core.Normalization.Methods.Abstraction;
using McdaToolkit.Core.Normalization.Service.Abstraction;
using McdaToolkit.Core.Extensions;

namespace McdaToolkit.Core.Normalization.Service;

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
        foreach (var (col, index) in matrix.EnumerateColumns().Indexed())
            matrix.SetColumn(columnIndex: index,
                column: criteriaTypes[index] == 1
                    ? _vectorNormalizatorMethod.Normalize(data: col, cost: false)
                    : _vectorNormalizatorMethod.Normalize(data: col, cost: true));
        return matrix;
    }

    public NormalizationMethod GetCurrentNormalizationName { get; }
    
    public Result ChangeNormalizationMethod(NormalizationMethod newMethod)
    {
        if (newMethod == GetCurrentNormalizationName)
        {
            return Result.Fail(new NormalizationMethodsEqualError());
        }
        _vectorNormalizatorMethod = NormalizationMethodFactory.Create(newMethod);
        return Result.Ok();
    }
}