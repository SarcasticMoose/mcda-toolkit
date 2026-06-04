using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Normalization.Abstractions;

/// <summary>
/// Defines a contract for vector normalization strategies.
/// </summary>
/// <typeparam name="T">
/// Element type of the <see cref="Vector{T}"/>. Must be <see cref="IFloatingPointIeee754{T}"/>.
/// </typeparam>
/// <remarks>
/// Implementations encapsulate a specific normalization method
/// and can be used interchangeably within a processing pipeline.
/// </remarks>
public interface IVectorNormalizer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>
    /// Gets the normalization method implemented by this normalizer.
    /// </summary>
    NormalizationMethod Implements { get; }

    /// <summary>
    /// Normalizes the specified vector according to the implemented strategy.
    /// </summary>
    /// <param name="data">
    /// Input vector to normalize.
    /// </param>
    /// <returns>
    /// A new <see cref="Vector{T}"/> instance containing normalized values.
    /// </returns>
    MathNet.Numerics.LinearAlgebra.Vector<T> Normalize(MathNet.Numerics.LinearAlgebra.Vector<T> data);
}
