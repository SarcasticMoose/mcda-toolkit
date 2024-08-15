using LightResults;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Extensions;
using McdaToolkit.Mcda.Services;

namespace McdaToolkit.Mcda.Methods.Abstraction;

public  abstract class McdaMethod : IMcdaMethod
{
    private readonly MatrixCheckerService _matrixCheckerService = new();
    
    protected abstract Result<Vector<double>> RunCalculation(double[,] matrix, double[] weights, int[] criteriaDirections);
    
    /// <inheritdoc cref="ICalculation{TValue}.Calculate(System.Collections.Generic.IEnumerable{System.Collections.Generic.IEnumerable{TValue}},System.Collections.Generic.IEnumerable{TValue},System.Collections.Generic.IEnumerable{int})"/>
    public Result<Vector<double>> Calculate(IEnumerable<IEnumerable<double>> matrix, IEnumerable<double> weights, IEnumerable<int> criteriaDirections)
    {
        var convertedMatrix = matrix.To2DArray();
        var convertedWeights = weights.ToArray();
        var convertedCriteriaDecision = criteriaDirections.ToArray();
        var matrixChecked = _matrixCheckerService.ValidateData(convertedMatrix, convertedWeights, convertedCriteriaDecision);
        
        if (matrixChecked.IsFailed)
        {
            return Result.Fail<Vector<double>>();
        }
        return RunCalculation(convertedMatrix, convertedWeights, convertedCriteriaDecision);
    }

    /// <summary>
    /// Calculate provided data
    /// </summary>
    /// <param name="matrix">Data as set of alternatives and theirs attributes</param>
    /// <param name="weights">Data determining relevance of each attribute</param>
    /// <param name="criteriaDirections">Data that determines the columns is profit or cost</param>
    /// <returns>Vector of processed data in descending order</returns>
    public Result<Vector<double>> Calculate(double[,] matrix,double[] weights,int[] criteriaDirections)
    {
        var matrixChecked = _matrixCheckerService.ValidateData(matrix, weights, criteriaDirections);
        if (matrixChecked.IsFailed)
        {
            return Result.Fail<Vector<double>>();
        }
        return RunCalculation(matrix, weights, criteriaDirections);
    }
}