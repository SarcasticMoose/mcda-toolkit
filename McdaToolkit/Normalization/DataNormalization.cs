using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Enums;
using McdaToolkit.Extensions;
using McdaToolkit.Normalization.Abstraction;
using McdaToolkit.NormalizationMethods.Abstraction;
using McdaToolkit.Options;

namespace McdaToolkit.Normalization;


internal class DataNormalization(NormalizationMethod method) : IDataNormalization
{
    private readonly INormalizationMethod _method = NormalizationFactory.CreateNormalizationMethod(method);

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
