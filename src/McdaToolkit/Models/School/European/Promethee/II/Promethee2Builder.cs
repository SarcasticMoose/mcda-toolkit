using McdaToolkit.Data.Normalization;
using McdaToolkit.Data.Normalization.Services.MatrixNormalizator;
using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;
using McdaToolkit.Models.School.European.Promethee.Preference.Functions.FShape;

namespace McdaToolkit.Models.School.European.Promethee.II;

public sealed class Promethee2Builder
{
    private NormalizationMethod _normalizationMethod;

    private IPreferenceFunction _preferenceFunction;

    public static Promethee2Builder Create() => new Promethee2Builder();
    
    public Promethee2Builder WithNormalizationMethod(NormalizationMethod normalizationMethod)
    {
        _normalizationMethod = normalizationMethod;
        return this;
    }

    public Promethee2Builder WithPreferenceFunction<TBuilder>(Action<TBuilder> action)
        where TBuilder : IPreferenceFunctionBuilder<IPreferenceFunction>, new()
    {
        var instance = new TBuilder();
        action.Invoke(instance); 
        _preferenceFunction = instance.Build();
        return this;
    }
    
    public Promethee2 Build()
    {
        var normalizationMethod = new NormalizationMethodFactory().Create(_normalizationMethod);
        var normalizationService = new MatrixNormalizatorService(normalizationMethod);
        return new Promethee2(normalizationService, _preferenceFunction);
    }
}