using McdaToolkit.Enums;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Normalization.Methods.Linear;
using McdaToolkit.Normalization.Methods.Sum;

namespace McdaToolkit;

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