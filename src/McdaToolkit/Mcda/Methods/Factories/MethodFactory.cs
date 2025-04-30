using McdaToolkit.Mcda.Methods.Builders;
using McdaToolkit.Mcda.Methods.Promethee2;
using McdaToolkit.Mcda.Methods.Topsis;
using McdaToolkit.Mcda.Methods.Vikor;

namespace McdaToolkit.Mcda.Methods.Factories;

internal static class MethodFactory
{
    public static Vikor.Vikor CreateVikor(VikorOptions options)
    {
        return new VikorBuilder()
            .WithNormalizationMethod(options.NormalizationMethod)
            .WithVParameter(options.VikorParameters.V)
            .Build();
    }
    
    public static Topsis.Topsis CreateTopsis(TopsisOptions options)
    {
        return new TopsisBuilder()
            .WithNormalizationMethod(options.NormalizationMethod)
            .Build();
    }
    
    public static Promethee2.Promethee2 CreatePromethee2(Promethee2Options options)
    {
        return new Promethee2Builder()
            .WithNormalizationMethod(options.NormalizationMethod)
            .WithPreferenceFunction(options.PreferenceFunction)
            .Build();
    }
  
}