using McdaToolkit.Models.Ranking;

namespace McdaToolkit.Models.Mcda;

/// <summary>Holds the result of a solved MCDA problem.</summary>
public class McdaSolved<T> where T : struct, IEquatable<T>, IComparable<T>
{
    /// <summary>The ranking of alternatives produced by the MCDA method.</summary>
    public Ranking<T> Ranking { get; internal set; }
}