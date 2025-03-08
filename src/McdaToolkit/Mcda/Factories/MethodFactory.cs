using McdaToolkit.Mcda.Factories.Enums;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Methods.Topsis;
using McdaToolkit.Mcda.Methods.Vikor;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Mcda.Factories;

public static class MethodFactory
{
    private static IMcdaMethod CreateMethod(
        McdaMethods method,
        IMcdaMethodOptions options)
    {
        var normalizationMatrixService = new MatrixNormalizatorService(
            new NormalizationMethodFactory(),
            options.NormalizationMethod);
        
        return method switch
        {
            McdaMethods.Vikor => new Vikor(normalizationMatrixService,((VikorOptions)options).VikorParameters),
            McdaMethods.Topsis => new Topsis(normalizationMatrixService),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    public static Vikor CreateVikor(VikorOptions options)
    {
        return (Vikor)CreateMethod(McdaMethods.Vikor, options);
    }
    
    public static Topsis CreateTopsis(TopsisOptions options)
    {
        return (Topsis)CreateMethod(McdaMethods.Topsis, options);
    }
}