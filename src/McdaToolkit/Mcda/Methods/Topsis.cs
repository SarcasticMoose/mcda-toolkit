using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Options;
using McdaToolkit.Normalization.Service;
using McdaToolkit.Normalization.Service.Abstraction;

namespace McdaToolkit.Mcda.Methods;

public sealed class Topsis : McdaMethod
{
    private readonly IMatrixNormalizationService _normalizationServiceServiceService;
    
    public Topsis(McdaMethodOptions options)
    {
        _normalizationServiceServiceService = new MatrixNormalizatorService(options.NormalizationMethod);
    }
    
    private Matrix<double> WeightedMatrix(Matrix<double> matrix, Vector<double> weights)
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

    protected override Result<Vector<double>> RunCalculation(double[,] matrix,double[] weights,int[] criteriaDirections)
    {
        var matrixBuilded = Matrix<double>.Build.DenseOfArray(matrix);
        var weightsBuilded = Vector<double>.Build.DenseOfArray(weights);
        var normalizedMatrix = _normalizationServiceServiceService.NormalizeMatrix(matrixBuilded, criteriaDirections);
        var weightedMatrix = WeightedMatrix(normalizedMatrix, weightsBuilded);

        var idealBest = IdealValues(weightedMatrix, true);
        var idealWorst = IdealValues(weightedMatrix, false);

        var distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        var distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        var topsisScores = CalculateTopsisScores(distanceToBest, distanceToWorst);

        return Result.Ok(topsisScores);
    }
}