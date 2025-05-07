using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions.Abstraction;
using McdaToolkit.Mcda.Ranking;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Mcda.Methods.Promethee2;

public sealed class Promethee2 : IMcdaMethod<Ranking<double>>
{
    private readonly MatrixNormalizatorService _normalizationServiceServiceService;
    private readonly IPreferenceFunction _preferenceFunction;

    internal Promethee2(
        MatrixNormalizatorService matrixNormalizationServiceService,
        IPreferenceFunction preferenceFunction)
    {
        _normalizationServiceServiceService = matrixNormalizationServiceService;
        _preferenceFunction = preferenceFunction;
    }
    
    public IResult<Ranking<double>> Run(McdaInputData data)
    {
        var normalizedMatrix = _normalizationServiceServiceService.NormalizeMatrix(data.Matrix, data.Types);
        var diffrentialMatrix = GetAlternativeDiffrence(normalizedMatrix);
        var afterPreferenceFunction = _preferenceFunction.Execute(diffrentialMatrix);
        var weightedPreferenceMatrix = GetWeightedMatrix(afterPreferenceFunction, data.Weights);
        var flowMatrix = AggregatePreferenceFlows(weightedPreferenceMatrix, normalizedMatrix.RowCount);
        var leavingFlows = flowMatrix.RowSums() / (flowMatrix.RowCount - 1);
        var enteringFlows = flowMatrix.ColumnSums() / (flowMatrix.ColumnCount - 1);
        var netFlows = leavingFlows - enteringFlows;
        return Result.Ok(new RankingFactory().CreateRanking(netFlows));
    }

    private Matrix<double> GetAlternativeDiffrence(Matrix<double> matrix)
    {
        var rows = matrix.RowCount;
        var cols = matrix.ColumnCount;  
        var alternativeMatrix = Matrix<double>.Build.Dense((rows * rows) - rows, cols);

        var iindex = 0;
        for (int i = 0; i < rows; i++)
        {
            var row = matrix.Row(i);
            for (int j = 0; j < rows; j++)
            {
                if(i == j) continue;
                var nextRow = row - matrix.Row(j);
                alternativeMatrix.SetRow(iindex, nextRow);
                iindex++;
            }
        }
        return alternativeMatrix;
    }

    private Matrix<double> GetWeightedMatrix(Matrix<double> matrix, Vector<double> weights)
    {
        for (int i = 0; i < matrix.RowCount; i++)
        {
            matrix.SetRow(i, matrix.Row(i).PointwiseMultiply(weights));
        }
        return matrix;
    }
    
    private Matrix<double> AggregatePreferenceFlows(Matrix<double> weightedMatrix, int alternativesCount)
    {
        var flowMatrix = Matrix<double>.Build.Dense(alternativesCount, alternativesCount);
        int k = 0;
        for (int i = 0; i < alternativesCount; i++)
        {
            for (int j = 0; j < alternativesCount; j++)
            {
                if (i == j) continue;
                var preference = weightedMatrix.Row(k).Sum();
                flowMatrix[i, j] = preference;
                k++;
            }
        }
        return flowMatrix;
    }
}