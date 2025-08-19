using McdaToolkit.Data.Operations;
using McdaToolkit.Data.Operations.Normalization;

namespace McdaToolkit.Models.School.European.Topsis;

public sealed class TopsisBuilder
{
    private NormalizationMethod _normalizationMethod;

    public static TopsisBuilder Create() => new TopsisBuilder();
    
    public TopsisBuilder WithNormalizationMethod(NormalizationMethod normalizationMethod)
    {
        _normalizationMethod = normalizationMethod;
        return this;
    }
    
    public Topsis Build()
    {
        var normalizationMethod = new NormalizationMethodFactory().Create(_normalizationMethod);
        var normalizationService = new MatrixOperations(normalizationMethod);
        return new Topsis(normalizationService);
    }
}