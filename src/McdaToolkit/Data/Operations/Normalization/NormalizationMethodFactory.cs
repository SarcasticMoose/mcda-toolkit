using McdaToolkit.Data.Operations.Normalization.Methods.Abstraction;
using McdaToolkit.Data.Operations.Normalization.Methods.Geometric;
using McdaToolkit.Data.Operations.Normalization.Methods.Linear;
using McdaToolkit.Data.Operations.Normalization.Methods.NonLinear;

namespace McdaToolkit.Data.Operations.Normalization;

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