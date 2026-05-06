using LightResults;
using McdaToolkit.Models.Mcda;
using McdaToolkit.Models.Ranking;

namespace McdaToolkit.Models.Abstraction;

/// <summary>Contract for MCDA methods that produce a ranked list of alternatives.</summary>
public interface IMcdaMethod<T>
    where T : struct, IEquatable<T>, IComparable<T>, IFormattable
{
    /// <summary>Executes the method on the given problem and returns a ranked result.</summary>
    Result<Ranking<T>> Run(
        McdaProblem<T> data,
        McdaExecutionOptions? options = null);
}
