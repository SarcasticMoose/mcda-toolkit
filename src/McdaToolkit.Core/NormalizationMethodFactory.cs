using McdaToolkit.Core.Enums;
using McdaToolkit.Core.Normalization.Methods.Abstraction;
using McdaToolkit.Core.Normalization.Methods.Linear;
using McdaToolkit.Core.Normalization.Methods.Sum;

namespace McdaToolkit.Core;

internal static class NormalizationMethodFactory
{
    public static IVectorNormalizator<double> Create(NormalizationMethod method)
    {
        return method switch
        {
            NormalizationMethod.MinMax => new MinMaxNormalization(),
            NormalizationMethod.Vector => new VectorNormalization(),
            NormalizationMethod.Logarithmic => new LogarithmicNormalization(),
            NormalizationMethod.Sum => new SumNormalization(),
            NormalizationMethod.Max => new MaxNormalization(),
            _ => throw new Exception("Not existing normalization")
        };
    }
}