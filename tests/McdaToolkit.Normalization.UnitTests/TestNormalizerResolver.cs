using System.Numerics;
using McdaToolkit.Normalization.Abstractions;
using McdaToolkit.Normalization.Normalizers;

namespace McdaToolkit.Normalization.UnitTests;

public sealed class TestNormalizerResolver<T> : INormalizerResolver<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public NormalizationMethod? LastResolvedMethod { get; private set; }

    public IVectorNormalizer<T> Resolve(NormalizationMethod method)
    {
        LastResolvedMethod = method;
        return method switch
        {
            NormalizationMethod.MinMax => new MinMaxNormalizer<T>(),
            NormalizationMethod.Max => new MaxNormalizer<T>(),
            NormalizationMethod.Sum => new SumNormalizer<T>(),
            NormalizationMethod.Vector => new VectorL2Normalizer<T>(),
            NormalizationMethod.Logarithmic => new LogarithmicNormalizer<T>(),
            _ => throw new ArgumentOutOfRangeException(nameof(method))
        };
    }
}
