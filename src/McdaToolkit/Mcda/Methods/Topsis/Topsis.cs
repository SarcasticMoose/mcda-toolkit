using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Providers;
using McdaToolkit.Normalization.Services.Abstraction;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Mcda.Methods.Topsis;

public sealed class Topsis : ITopsisMethod
{
    private readonly IMatrixNormalizationService _normalizationServiceServiceService;
    
    private Topsis(McdaMethodOptions options)
    {
        _normalizationServiceServiceService = new MatrixNormalizatorService(options.NormalizationMethod);
    }

    internal static Topsis Create(McdaMethodOptions options)
    {
        return new Topsis(options);
    }
    
    private Vector<double> IdealValues(Matrix<double> matrix, bool pis)
    {
        return Vector<double>.Build
            .Dense(matrix.ColumnCount, i =>
            {
                var columnValues = matrix.Column(i);
                return pis ? columnValues.Max() : columnValues.Min();
            });
    }

    private Vector<double> CalculateEuclideanDistance(Matrix<double> matrix, Vector<double> ideal)
    {
        return Vector<double>.Build
            .DenseOfArray(matrix
                .EnumerateRows()
                .Select(row => row
                    .Subtract(ideal)
                    .PointwisePower(2)
                    .PointwiseSqrt()
                    .Sum())
                .ToArray());
    }

    private Vector<double> CalculateTopsisScores(Vector<double> distanceToBest, Vector<double> distanceToWorst)
    {
        return distanceToWorst.PointwiseDivide(distanceToBest.Add(distanceToWorst));
    }

    private IResult<TopsisScore> ComputeScore(Matrix<double> matrix, Vector<double> weights, int[] criteriaDecisions)
    {
        var normalizedMatrix = _normalizationServiceServiceService.NormalizeMatrix(matrix, criteriaDecisions);
        var weightedMatrix = normalizedMatrix.MapIndexed((i, j, value) => weights[j] * matrix[i, j]);

        var idealBest = IdealValues(weightedMatrix, true);
        var idealWorst = IdealValues(weightedMatrix, false);

        var distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        var distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        var scores = CalculateTopsisScores(distanceToBest, distanceToWorst);

        return Result.Ok(new TopsisScore(scores));
    }
    
    public IResult<TopsisScore> Run(IDataProvider dataProvider)
    {
        var data = dataProvider.GetData();
        return ComputeScore(data.Matrix,data.Weights,data.Types);
    }

    IResult IMcdaMethod.Run(IDataProvider dataProvider)
    {
        return Run(dataProvider);
    }
}