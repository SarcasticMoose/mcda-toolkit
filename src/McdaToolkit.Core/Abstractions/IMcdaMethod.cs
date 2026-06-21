using System.Numerics;

namespace McdaToolkit.Core.Abstractions;

/// <summary>Contract for MCDA methods that compute per-alternative scores.</summary>
public interface IMcdaMethod<T>
    where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>Computes per-alternative scores for the given problem.</summary>
    T[] Execute(McdaProblem<T> problem);
}
