using System.Numerics;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Normalization.Methods.Geometric;
using McdaToolkit.Normalization.Methods.Linear;
using McdaToolkit.Normalization.Methods.NonLinear;

namespace McdaToolkit.Normalization.UnitTests;

public sealed class TestNormalizerResolver : INormalizerResolver
{
    public NormalizationMethod? LastResolvedMethod { get; private set; }

    public IVectorNormalizer<T> Resolve<T>(NormalizationMethod method)
        where T : struct, IFloatingPointIeee754<T>
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