using McdaToolkit.Data.Operations;
using McdaToolkit.Data.Operations.Normalization;
using McdaToolkit.Models.School.European.Promethee.Gaia;
using McdaToolkit.Models.School.European.Promethee.PreferenceFunctions.Factory;

namespace McdaToolkit.Models.School.European.Promethee.II.Gaia;

public sealed class Promethee2GaiaBuilder
{
    private NormalizationMethod _normalizationMethod;

    private PreferenceFunction _preferenceFunction;

    public static Promethee2GaiaBuilder Create() => new();
    
    public Promethee2GaiaBuilder WithNormalizationMethod(NormalizationMethod normalizationMethod)
    {
        _normalizationMethod = normalizationMethod;
        return this;
    }

    public Promethee2GaiaBuilder WithPreferenceFunction(PreferenceFunction preferenceFunction)
    {
        _preferenceFunction = preferenceFunction;
        return this;
    }
    
    public Promethee2Gaia Build()
    {
        var normalizationMethod = new NormalizationMethodFactory().Create(_normalizationMethod);
        var matrixOperations = new MatrixOperations(normalizationMethod);
        var preferenceFunction = new PreferenceFunctionFactory().Create(_preferenceFunction);
        var gaiaProcessor = new GaiaProcessor(new PrincipalComponentAnalysis());
        var prometheeBase = new Promethee2Base();
        return new Promethee2Gaia(matrixOperations, matrixOperations, gaiaProcessor, preferenceFunction, prometheeBase);
    }
}