
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Enums;
using McdaToolkit.Extensions;
using McdaToolkit.Normalization.Interfaces;
using McdaToolkit.NormalizationMethods.Interfaces;
namespace McdaToolkit.Normalization;


public class DataNormalizationService(NormalizationMethodEnum methodEnum) : IDataNormalization
{
    private readonly INormalize<double> _method = NormalizationMethodFactory.Create(methodEnum);

    public Matrix<double> NormalizeMatrix(Matrix<double> matrix, int[] criteriaTypes)
    {
        foreach (var (col,index) in matrix.EnumerateColumns().Indexed())
        {
            matrix.SetColumn(index, 
                criteriaTypes[index] == 1 
                    ? _method.Normalize(data: col, cost: false)
                    : _method.Normalize(data: col, cost: true));
        }
        return matrix;
    }
}
