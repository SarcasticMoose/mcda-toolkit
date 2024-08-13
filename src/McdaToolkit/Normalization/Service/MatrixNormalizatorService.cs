using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Enums;
using McdaToolkit.Extensions;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Normalization.Service.Abstraction;

namespace McdaToolkit.Normalization.Service;

public sealed class MatrixNormalizatorService : IMatrixNormalizationService
{
    private IVectorNormalizator<double> _vectorNormalizatorMethod;

    public MatrixNormalizatorService(NormalizationMethod name)
    {
        GetCurrentNormalizationName = name;
        _vectorNormalizatorMethod = NormalizationMethodFactory.Create(GetCurrentNormalizationName);
    }

    /// <inheritdoc cref="IMatrixNormalizator{double}.NormalizeMatrix"/>
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
            return Result.Fail(NormalizationServiceErrors.MethodsEqual());
        }
        _vectorNormalizatorMethod = NormalizationMethodFactory.Create(newMethod);
        return Result.Ok();
    }
}