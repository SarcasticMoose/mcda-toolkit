using McdaToolkit.Mcda.Factories.Enums;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Methods.Topsis;
using McdaToolkit.Mcda.Methods.Vikor;

namespace McdaToolkit.Mcda.Factories;

public static class MethodFactory
{
    private static IMcdaMethod CreateMethod(McdaMethods method,McdaMethodOptions options)
    {
        return method switch
        {
            McdaMethods.VIKOR => Vikor.Create(options),
            McdaMethods.TOPSIS => Topsis.Create(options),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    public static IVikorMethod CreateVikor(McdaMethodOptions options)
    {
        return (IVikorMethod)CreateMethod(McdaMethods.VIKOR, options);
    }
    
    public static ITopsisMethod CreateTopsis(McdaMethodOptions options)
    {
        return (ITopsisMethod)CreateMethod(McdaMethods.TOPSIS, options);
    }
}