using System.Numerics;
using McdaToolkit.Enums;
using McdaToolkit.NormalizationMethods;
using McdaToolkit.NormalizationMethods.Abstraction;

namespace McdaToolkit;

internal static class NormalizationFactory
{
    public static INormalizationMethod CreateNormalizationMethod(NormalizationMethod method)
    {
        return method switch
        {
            NormalizationMethod.MinMax => new MinMaxNormalization(),
            _ => throw new Exception("Not existing normalization")
        };
    }
}