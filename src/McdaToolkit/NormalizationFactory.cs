using System.Numerics;
using McdaToolkit.Enums;
using McdaToolkit.NormalizationMethods;
using McdaToolkit.NormalizationMethods.Interfaces;
using McdaToolkit.NormalizationMethods.Types.Linear;
using McdaToolkit.NormalizationMethods.Types.Sum;

namespace McdaToolkit;

internal static class NormalizationFactory
{
    public static INormalize<double> CreateNormalizationMethod(NormalizationMethodEnum methodEnum)
    {
        return methodEnum switch
        {
            NormalizationMethodEnum.MinMax => new MinMaxNormalization(),
            NormalizationMethodEnum.Vector => new VectorNormalization(),
            NormalizationMethodEnum.Logarithmic => new LogarithmicNormalization(),
            _ => throw new Exception("Not existing normalization")
        };
    }
}