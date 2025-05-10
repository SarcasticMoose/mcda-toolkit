using McdaToolkit.Methods.Promethee2.PreferenceFunctions.Factory;
using McdaToolkit.Normalization.Enums;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Methods.Promethee2;

internal sealed class Promethee2Builder
{
    private NormalizationMethod _normalizationMethod;

    private PreferenceFunction _preferenceFunction;

    public Promethee2Builder WithNormalizationMethod(NormalizationMethod normalizationMethod)
    {
        _normalizationMethod = normalizationMethod;
        return this;
    }

    public Promethee2Builder WithPreferenceFunction(PreferenceFunction preferenceFunction)
    {
        _preferenceFunction = preferenceFunction;
        return this;
    }
    
    public Promethee2 Build()
    {
        var normalizationMethod = new NormalizationMethodFactory().Create(_normalizationMethod);
        var normalizationService = new MatrixNormalizatorService(normalizationMethod);
        var preferenceFunction = new PreferenceFunctionFactory().Create(_preferenceFunction);
        return new Promethee2(normalizationService, preferenceFunction);
    }
}