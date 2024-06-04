using System.Numerics;
using McdaToolkit.Enums;
using McdaToolkit.NormalizationMethods;
using McdaToolkit.NormalizationMethods.Interfaces;

namespace McdaToolkit;

internal static class NormalizationFactory
{
    public static INormalizationMethod CreateNormalizationMethod(NormalizationMethodEnum methodEnum)
    {
        return methodEnum switch
        {
            NormalizationMethodEnum.MinMax => new MinMaxNormalization(),
            _ => throw new Exception("Not existing normalization")
        };
    }
}