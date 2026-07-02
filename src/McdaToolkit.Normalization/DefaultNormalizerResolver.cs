using System.Numerics;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization;

internal sealed class DefaultNormalizerResolver<T>(IEnumerable<IVectorNormalizer<T>> normalizers) : INormalizerResolver<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly IEnumerable<IVectorNormalizer<T>> _normalizers = normalizers;

    public IVectorNormalizer<T> Resolve(NormalizationMethod method)
        => _normalizers.Single(n => n.Implements == method);
}
