using System.Numerics;
using McdaToolkit.Core.Ranking;

namespace McdaToolkit.Core;

/// <summary>Holds the result of a solved MCDA problem.</summary>
public record McdaSolved<T>
where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>The ranking of alternatives produced by the MCDA method.</summary>
    public Ranking<T> Ranking { get; init; }
}
