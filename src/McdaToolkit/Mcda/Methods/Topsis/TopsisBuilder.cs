using McdaToolkit.Normalization.Enums;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Mcda.Methods.Topsis;

public class TopsisBuilder
{
    private NormalizationMethod _normalizationMethod;

    public TopsisBuilder WithNormalizationMethod(NormalizationMethod normalizationMethod)
    {
        _normalizationMethod = normalizationMethod;
        return this;
    }
    
    public Topsis Build()
    {
        var normalizationMethod = new NormalizationMethodFactory().Create(_normalizationMethod);
        var normalizationService = new MatrixNormalizatorService(normalizationMethod);
        return new Topsis(normalizationService);
    }
}