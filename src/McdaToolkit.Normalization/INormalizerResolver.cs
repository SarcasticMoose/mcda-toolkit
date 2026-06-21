using System.Numerics;
using McdaToolkit.Normalization.Abstractions;

namespace McdaToolkit.Normalization;

/// <summary>Resolves a <see cref="IVectorNormalizer{T}"/> for a given <see cref="NormalizationMethod"/>.</summary>
/// <typeparam name="T">
/// Floating-point numeric type used by the decision problem.
/// </typeparam>
public interface INormalizerResolver<T>
    where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>Returns the normalizer implementation for the specified method.</summary>
    IVectorNormalizer<T> Resolve(NormalizationMethod method);
}
