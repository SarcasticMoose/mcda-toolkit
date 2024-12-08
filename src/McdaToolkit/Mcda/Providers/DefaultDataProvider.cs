using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Extensions;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Services.MatrixChecker;

namespace McdaToolkit.Mcda.Providers;

internal class DefaultDataProvider : IDataProvider
{
    private Matrix<double> _matrix;
    private Vector<double> _weights;
    private int[] _types;
    private IMcdaAdditionalParameters? _additionalParameters;

    private readonly MatrixCheckerService _matrixCheckerService;
    public DefaultDataProvider(MatrixCheckerService matrixCheckerService)
    {
        _matrixCheckerService = matrixCheckerService;
    }

    public IResult ProvideData(IEnumerable<IEnumerable<double>> matrix, IEnumerable<double> weights, IEnumerable<int> criteriaTypes)
    {
        return ProvideData(matrix.To2DArray(),weights.ToArray(),criteriaTypes.ToArray());
    }

    internal McdaInputData GetData() => new(_matrix, _weights, _types);

    public IResult ProvideData(double[,] matrix, double[] weights, int[] criteriaTypes)
    {
        var matrixChecked = _matrixCheckerService.ValidateData(matrix, weights, criteriaTypes);

        if (matrixChecked.IsFailed)
        {
            return matrixChecked;
        }
        _matrix = Matrix<double>.Build.DenseOfArray(matrix);
        _weights = Vector<double>.Build.DenseOfArray(weights);
        _types = criteriaTypes;
        return Result.Ok();
    }
}