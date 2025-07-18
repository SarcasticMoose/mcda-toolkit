using McdaToolkit.Data.Normalization;
using McdaToolkit.Data.Normalization.Methods.Abstraction;
using McdaToolkit.Data.Normalization.Methods.Geometric;
using McdaToolkit.Data.Normalization.Methods.Linear;
using McdaToolkit.Data.Normalization.Methods.NonLinear;

namespace McdaToolkit;

internal class NormalizationMethodFactory
{
    public IVectorNormalizator<double> Create(NormalizationMethod method)
    {
        return method switch
        {
            NormalizationMethod.MinMax => new MinMaxNormalization(),
            NormalizationMethod.Vector => new VectorL2Normalization(),
            NormalizationMethod.Logarithmic => new LogarithmicNormalization(),
            NormalizationMethod.Sum => new SumNormalization(),
            NormalizationMethod.Max => new MaxNormalization(),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }
}