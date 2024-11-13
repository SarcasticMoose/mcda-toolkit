using LightResults;
using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Providers;

public interface IDataProvider
{
    public IResult ProvideData(double [,] matrix,
        double[] weights, 
        int[] criteriaTypes, 
        IMcdaAdditionalParameters? additionalParameters = null);
    public McdaInputData GetData();
}