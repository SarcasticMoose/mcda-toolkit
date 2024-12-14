using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Extensions;

namespace McdaToolkit.Mcda.Providers;

internal class DefaultDataProvider
{
    private Matrix<double> _matrix;
    private Vector<double> _weights;
    private int[] _types;
    
    internal McdaInputData GetData() => new(_matrix, _weights, _types);

    internal IResult ProvideData(double[,] matrix, double[] weights, int[] criteriaTypes)
    {
        _matrix = Matrix<double>.Build.DenseOfArray(matrix);
        _weights = Vector<double>.Build.DenseOfArray(weights);
        _types = criteriaTypes;
        return Result.Ok();
    }
}