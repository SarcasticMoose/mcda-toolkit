using System.Numerics;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization;

/// <summary>Resolves a <see cref="IVectorNormalizer{T}"/> for a given <see cref="NormalizationMethod"/>.</summary>
public interface INormalizerResolver
{
    /// <summary>Returns the normalizer implementation for the specified method.</summary>
    IVectorNormalizer<T> Resolve<T>(NormalizationMethod method)
        where T : struct, IFloatingPointIeee754<T>;
}
