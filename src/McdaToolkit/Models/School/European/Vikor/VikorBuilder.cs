using McdaToolkit.Data.Normalization;
using McdaToolkit.Data.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Models.School.European.Vikor;

public sealed class VikorBuilder 
{
    private NormalizationMethod _normalizationMethod;
    private double _v;
    
    public static VikorBuilder Create() => new VikorBuilder();
    public VikorBuilder WithNormalizationMethod(NormalizationMethod normalizationMethod)
    {
        _normalizationMethod = normalizationMethod;
        return this;
    }
    
    public VikorBuilder WithVParameter(double v)
    {
        _v = v;
        return this;
    }
    
    public Vikor Build()
    {
        var normalizationMethod = new NormalizationMethodFactory().Create(_normalizationMethod);
        var matrixNormalizationService = new MatrixNormalizatorService(normalizationMethod);
        
        return new Vikor(
            matrixNormalizationService,
            _v);
    }
}