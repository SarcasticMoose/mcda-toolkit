using McdaToolkit.Methods.Promethee.II;
using McdaToolkit.Methods.Topsis;
using McdaToolkit.Methods.Vikor;

namespace McdaToolkit.Shared.Factories;

internal static class MethodFactory
{
    public static Vikor CreateVikor(VikorOptions options)
    {
        return new VikorBuilder()
            .WithNormalizationMethod(options.NormalizationMethod)
            .WithVParameter(options.VikorParameters.V)
            .Build();
    }
    
    public static Topsis CreateTopsis(TopsisOptions options)
    {
        return new TopsisBuilder()
            .WithNormalizationMethod(options.NormalizationMethod)
            .Build();
    }
    
    public static Promethee2 CreatePromethee2(Promethee2Options options)
    {
        return new Promethee2Builder()
            .WithNormalizationMethod(options.NormalizationMethod)
            .WithPreferenceFunction(options.PreferenceFunction)
            .Build();
    }
  
}