using System.Numerics;
using McdaToolkit.Core.Abstractions;

namespace McdaToolkit.Extensions.Algorithms.Vikor.Result;

/// <summary>
/// Represent each alternative in <see cref="VikorMethodResult{T}"/>
/// </summary>
public readonly record struct VikorMethodAlternativeResult<T>
    where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>
    /// Maximum group utility (utility measure).
    /// Represents the sum of weighted normalized distances from the ideal solution
    /// across all criteria — lower values indicate better overall performance.
    /// </summary>
    public T S { get; init; }

    /// <summary>
    /// Minimum individual regret (regret measure).
    /// Represents the maximum weighted normalized distance from the ideal solution
    /// among all criteria — lower values indicate that no single criterion
    /// deviates significantly from the ideal.
    /// </summary>
    public T R { get; init; }

    /// <summary>
    /// VIKOR compromise index.
    /// A weighted aggregation of <see cref="S"/> and <see cref="R"/>,
    /// balancing group utility against individual regret via the decision-making
    /// mechanism coefficient v ∈ [0, 1]. Lower values indicate a better
    /// compromise solution. The alternative with the lowest Q is the
    /// top-ranked compromise candidate, subject to acceptable advantage
    /// and stability conditions.
    /// </summary>
    public T Q { get; init; }
}
