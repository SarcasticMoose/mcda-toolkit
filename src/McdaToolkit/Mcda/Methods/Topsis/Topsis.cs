using LightResults;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Normalization.Services.Abstraction;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Mcda.Methods.Topsis;

public sealed class Topsis : ITopsisMethod
{
    private readonly IMatrixNormalizationService _normalizationServiceServiceService;
    
    internal Topsis(MatrixNormalizatorService matrixNormalizationServiceService)
    {
        _normalizationServiceServiceService = matrixNormalizationServiceService;
    }
    
    private Vector<double> IdealValues(
        Matrix<double> matrix,
        bool pis)
    {
        return Vector<double>
            .Build
            .Dense(matrix.ColumnCount, i =>
            {
                var column = matrix.Column(i);
                return pis ? column.Max() : column.Min();
            });
    }

    private Vector<double> CalculateEuclideanDistance(
        Matrix<double> matrix,
        Vector<double> point)
    {
        return Vector<double>
            .Build
            .DenseOfArray(matrix
                .EnumerateRows()
                .Select(row => Distance.Euclidean(row, point))
                .ToArray());
    }
    
    private IResult<TopsisScore> ComputeScore(
        Matrix<double> matrix, 
        Vector<double> weights, 
        int[] criteriaDecisions)
    {
        var normalizedMatrix = _normalizationServiceServiceService.NormalizeMatrix(matrix, criteriaDecisions);
        var weightedMatrix = normalizedMatrix.MapIndexed((i, j, value) => weights[j] * matrix[i, j]);

        var idealBest = IdealValues(weightedMatrix, true);
        var idealWorst = IdealValues(weightedMatrix, false);

        var distanceToBest = CalculateEuclideanDistance(weightedMatrix, idealBest);
        var distanceToWorst = CalculateEuclideanDistance(weightedMatrix, idealWorst);
        var scores = distanceToWorst.PointwiseDivide(distanceToBest.Add(distanceToWorst));
        
        return Result.Ok(new TopsisScore(scores));
    }
    
    public IResult<TopsisScore> Run(McdaInputData data)
    {
        return ComputeScore(data.Matrix,data.Weights,data.Types);
    }

    IResult IMcdaMethod.Run(McdaInputData data)
    {
        return Run(data);
    }
}