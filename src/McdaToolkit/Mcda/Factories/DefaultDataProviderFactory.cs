using McdaToolkit.Mcda.Providers;
using McdaToolkit.Mcda.Services;
using McdaToolkit.Mcda.Services.MatrixChecker;

namespace McdaToolkit.Mcda.Factories;

public static class DefaultDataProviderFactory 
{
    public static IDataProvider CreateDataProvider()
    {
        MatrixCheckerService matrixCheckerService = new();
        return new DefaultDataProvider(matrixCheckerService);
    }
}