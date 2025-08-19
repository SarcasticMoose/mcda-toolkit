using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Models.School.European.Promethee.II;

internal class Promethee2Base
{
    internal Matrix<double> GetAlternativeDiffrence(Matrix<double> matrix)
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
    
    internal Matrix<double> AggregatePreferenceFlows(Matrix<double> weightedMatrix, int alternativesCount)
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