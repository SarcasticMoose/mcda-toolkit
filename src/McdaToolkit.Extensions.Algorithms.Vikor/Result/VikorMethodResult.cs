using System.Numerics;
using McdaToolkit.Core.Abstractions;

namespace McdaToolkit.Extensions.Algorithms.Vikor.Result;

/// <summary>
/// Represents the result of the VIKOR method calculation.
/// </summary>
/// <typeparam name="T">
/// The numeric type used in the calculations. Must implement
/// <see cref="IFloatingPointIeee754{TSelf}"/>.
/// </typeparam>
public sealed record VikorMethodResult<T> : IMcdaResult<T>
    where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>
    /// Gets the evaluation results for all analyzed alternatives.
    /// </summary>
    public required IReadOnlyCollection<VikorMethodAlternativeResult<T>> Alternatives { get; init; }

    /// <summary>
    /// Gets the threshold value (DQ) used to determine whether the advantage
    /// of the best-ranked alternative is sufficiently significant.
    /// </summary>
    public required T DQ { get; init; }

    /// <summary>
    /// Gets the indices of the alternatives that belong to the compromise set.
    /// </summary>
    public required IReadOnlyCollection<int> CompromiseSetIndices { get; init; }

    /// <summary>
    /// Gets the index of the selected best alternative if a unique compromise
    /// solution exists; otherwise, <see langword="null"/>.
    /// </summary>
    public required int? BestAlternativeIndex { get; init; }
}
