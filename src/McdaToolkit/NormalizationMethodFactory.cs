using System.Numerics;
using McdaToolkit.Enums;
using McdaToolkit.NormalizationMethods;
using McdaToolkit.NormalizationMethods.Interfaces;
using McdaToolkit.NormalizationMethods.Types.Linear;
using McdaToolkit.NormalizationMethods.Types.Sum;

namespace McdaToolkit;

internal static class NormalizationMethodFactory
{
    public static INormalize<double> Create(NormalizationMethodEnum methodEnum)
    {
        return methodEnum switch
        {
            NormalizationMethodEnum.MinMax => new MinMaxNormalization(),
            NormalizationMethodEnum.Vector => new VectorNormalization(),
            NormalizationMethodEnum.Logarithmic => new LogarithmicNormalization(),
            NormalizationMethodEnum.Sum => new SumNormalization() ,
            NormalizationMethodEnum.Max => new MaxNormalization(),
            _ => throw new Exception("Not existing normalization")
        };
    }
}