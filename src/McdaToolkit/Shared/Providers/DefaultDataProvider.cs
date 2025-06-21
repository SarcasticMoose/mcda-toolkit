using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Shared.Data;

namespace McdaToolkit.Shared.Providers;

internal sealed class DefaultDataProvider
{
    private Matrix<double> _matrix;
    private Vector<double> _weights;
    private int[] _types;

    public DefaultDataProvider(double[,] matrix, double[] weights, int[] criteriaTypes)
    {
        _matrix = Matrix<double>.Build.DenseOfArray(matrix);
        _weights = Vector<double>.Build.DenseOfArray(weights);
        _types = criteriaTypes;
    }
    
    internal McdaInputData GetData() => new(_matrix, _weights, _types);
}