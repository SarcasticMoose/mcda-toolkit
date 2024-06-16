using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Enums;
using McdaToolkit.McdaMethods.Abstraction;
using McdaToolkit.McdaMethods.Interfaces;
using McdaToolkit.Normalization;
using McdaToolkit.Options;

namespace McdaToolkit.McdaMethods;

public class TopsisMethod : McdaMethod
{
    private readonly DataNormalizationService _normalizationServiceService;

    public TopsisMethod()
    {
        _normalizationServiceService = new DataNormalizationService(NormalizationMethodEnum.MinMax);
    }

    public TopsisMethod(McdaMethodOptions options)
    {
        _normalizationServiceService = new DataNormalizationService(options.NormalizationMethodEnum);
    }
    
    protected override Result<Vector<double>> Calculate(Matrix<double> matrix, double[] weights,
        int[] criteriaDirections)
    {
        var normalizedMatrix = _normalizationServiceService.NormalizeMatrix(matrix, criteriaDirections);
        var weightedMatrix = WeightedMatrix(normalizedMatrix, weights);

        var idealBest = IdealValues(weightedMatrix, true);
        var idealWorst = IdealValues(weightedMatrix, false);

        var distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        var distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        var topsisScores = CalculateTopsisScores(distanceToBest, distanceToWorst);

        return Result.Ok(topsisScores);
    }

    private Matrix<double> WeightedMatrix(Matrix<double> matrix, double[] weights)
    {
        for (int i = 0; i < matrix.RowCount; i++)
        {
            for (int j = 0; j < matrix.ColumnCount; j++)
            {
                matrix[i, j] *= weights[j];
            }
        }

        return matrix;
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

    private Vector<double> CalculateEuclideanDistance(Matrix<double> matrix,
        Vector<double> ideal)
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
}