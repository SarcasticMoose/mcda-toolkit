using McdaToolkit.Ranking;

namespace McdaToolkit;

/// <summary>Holds the result of a solved MCDA problem.</summary>
public record McdaSolved<T> where T : struct, IEquatable<T>, IComparable<T>
{
    /// <summary>The ranking of alternatives produced by the MCDA method.</summary>
    public Ranking<T> Ranking { get; init; }
}