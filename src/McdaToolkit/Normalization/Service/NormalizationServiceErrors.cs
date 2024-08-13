using LightResults;

namespace McdaToolkit.Normalization.Service;

public static class NormalizationServiceErrors
{
    public static Error MethodsEqual() => new("New method have to be diffrent than current one");
}