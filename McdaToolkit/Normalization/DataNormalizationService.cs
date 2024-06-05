using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Enums;
using McdaToolkit.Extensions;
using McdaToolkit.Normalization.Interfaces;
using McdaToolkit.NormalizationMethods.Interfaces;
using McdaToolkit.Options;

namespace McdaToolkit.Normalization;


public class DataNormalizationService(NormalizationMethodEnum methodEnum) : IDataNormalization
{
    private readonly INormalizationMethod _method = NormalizationFactory.CreateNormalizationMethod(methodEnum);

    public Matrix<double> NormalizeMatrix(Matrix<double> matrix, int[] criteriaTypes)
    {
        var normalizedMatrix = Matrix<double>.Build.Dense(matrix.RowCount, matrix.ColumnCount);
        
        foreach (var (col,index) in matrix.EnumerateColumns().Indexed())
        {
            normalizedMatrix.SetColumn(index, 
                criteriaTypes[index] == 1 
                    ? _method.Normalize(data: col, cost: false)
                    : _method.Normalize(data: col, cost: true));
        }
        return normalizedMatrix;
    }
}
