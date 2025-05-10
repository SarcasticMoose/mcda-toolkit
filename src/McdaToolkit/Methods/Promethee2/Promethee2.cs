using LightResults;
using MathNet.Numerics.LinearAlgebra;
<<<<<<<< HEAD:src/McdaToolkit/Methods/Promethee/II/Promethee2.cs
using McdaToolkit.Data.Normalization.Services.MatrixNormalizator;
using McdaToolkit.Methods.Promethee.II.PreferenceFunctions.Abstraction;
========
using McdaToolkit.Methods.Promethee2.PreferenceFunctions.Abstraction;
using McdaToolkit.Normalization.Services.MatrixNormalizator;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Methods/Promethee2/Promethee2.cs
using McdaToolkit.Shared.Abstraction;
using McdaToolkit.Shared.Data;
using McdaToolkit.Shared.Ranking;

<<<<<<<< HEAD:src/McdaToolkit/Methods/Promethee/II/Promethee2.cs
namespace McdaToolkit.Methods.Promethee.II;
========
namespace McdaToolkit.Methods.Promethee2;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Methods/Promethee2/Promethee2.cs

public class Promethee2 : IMcdaMethod<Ranking<double>>
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
<<<<<<<< HEAD:src/McdaToolkit/Methods/Promethee/II/Promethee2.cs
        return Result.Success(netFlows.CreateRanking());
========
        return Result.Ok(netFlows.CreateRanking());
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Methods/Promethee2/Promethee2.cs
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