using McdaToolkit.Shared.Data;
using McdaToolkit.Shared.Validation.MatrixValidation;

namespace McdaToolkit.Shared.Providers;

public class  DefaultDataProviderBuilder
{
    private double[,] _matrix;
    private double[] _weigths;
    private int[] _criteriaDecision;
    
    public  DefaultDataProviderBuilder AddDecisionMatrix(double[,] matrix)
    {
        _matrix = matrix;
        return this;
    }
    
    public  DefaultDataProviderBuilder AddWeights(double[] weights)
    {
        _weigths = weights;
        return this;
    }
    
    public  DefaultDataProviderBuilder AddDecisionCriteria(int[] criteriaDecision)
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