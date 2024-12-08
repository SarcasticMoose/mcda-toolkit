using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Providers;
using McdaToolkit.Mcda.Services.MatrixChecker;

namespace McdaToolkit.Mcda.Factories;

public class DataProviderBuilder
{
    private double[,] _matrix;
    private double[] _weigths;
    private int[] _criteriaDecision;
    IMcdaAdditionalParameters? _additionalParameters;
    
    public DataProviderBuilder AddDecisionMatrix(double[,] matrix)
    {
        _matrix = matrix;
        return this;
    }
    
    public DataProviderBuilder AddWeights(double[] weights)
    {
        _weigths = weights;
        return this;
    }
    
    public DataProviderBuilder AddDecisionCriteria(int[] criteriaDecision)
    {
        _criteriaDecision = criteriaDecision;
        return this;
    }
    
    public McdaInputData Build()
    {
        MatrixCheckerService matrixCheckerService = new();
        var dataProvider = new DefaultDataProvider(matrixCheckerService);
        var provideResult = dataProvider.ProvideData(_matrix, _weigths, _criteriaDecision);
        if (provideResult.IsFailed)
        {
            throw new InvalidOperationException($"Failed to provide data because of error/errors: {provideResult.Errors}");
        }
        return dataProvider.GetData();
    }
}