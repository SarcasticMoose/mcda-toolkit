using McdaToolkit.Shared.Data;
using McdaToolkit.Shared.Validation.MatrixValidation;

namespace McdaToolkit.Shared.Providers;

public class  DataBuilder
{
    private double[,] _matrix;
    private double[] _weigths;
    private int[] _criteriaDecision;
    
    public  DataBuilder AddDecisionMatrix(double[,] matrix)
    {
        _matrix = matrix;
        return this;
    }
    
    public  DataBuilder AddWeights(double[] weights)
    {
        _weigths = weights;
        return this;
    }
    
    public  DataBuilder AddDecisionCriteria(int[] criteriaDecision)
    {
        _criteriaDecision = criteriaDecision;
        return this;
    }
    
    public McdaInputData Build()
    {
        var matrixValidationResult = new MatrixValidation(_matrix, _weigths, _criteriaDecision).Validate();
        if (matrixValidationResult.IsFailure())
        {
            throw new ArgumentNullException($"Failed to provide data because of: {string .Join(", ",matrixValidationResult.Errors)}");
        }
        var provider = new DefaultDataProvider(_matrix, _weigths, _criteriaDecision);
        return provider.GetData();
    }
}