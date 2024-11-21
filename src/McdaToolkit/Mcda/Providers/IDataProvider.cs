using LightResults;
using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Providers;

public interface IDataProvider
{
    public IResult ProvideData(double [,] matrix,
        double[] weights, 
        int[] criteriaTypes, 
        IMcdaAdditionalParameters? additionalParameters = null);
    
    public IResult ProvideData(IEnumerable<IEnumerable<double>> matrix,
        IEnumerable<double> weights, 
        IEnumerable<int> criteriaTypes, 
        IMcdaAdditionalParameters? additionalParameters = null);
    
    internal McdaInputData GetData();
}