using McdaToolkit.Data.Operations;
using McdaToolkit.Data.Operations.Normalization;

namespace McdaToolkit.Models.School.European.Vikor;

public sealed class VikorBuilder 
{
    private NormalizationMethod _normalizationMethod;
    private VikorParameters _vikorParameters;
    
    public static VikorBuilder Create() => new VikorBuilder();
    public VikorBuilder WithNormalizationMethod(NormalizationMethod normalizationMethod)
    {
        _normalizationMethod = normalizationMethod;
        return this;
    }
    
    public VikorBuilder WithParameters(Action<VikorParametersBuilder> builder)
    {
        var vikorParameterBuilder = VikorParametersBuilder.Create();
        builder.Invoke(vikorParameterBuilder);
        
        _vikorParameters = vikorParameterBuilder.Build();
        return this;
    }
    
    public Vikor Build()
    {
        var normalizationMethod = new NormalizationMethodFactory().Create(_normalizationMethod);
        var matrixOperations = new MatrixOperations(normalizationMethod);
        
        return new Vikor(
            matrixOperations,
            _vikorParameters);
    }
}