using LightResults;
using McdaToolkit.Models.Mcda;
using McdaToolkit.Models.Ranking;

namespace McdaToolkit.Models.Abstraction;

/// <summary>Base class for MCDA methods providing a standard run-and-rank flow.</summary>
public abstract class McdaMethod<T> : IMcdaMethod<T>
    where T : struct, IEquatable<T>, IComparable<T>, IFormattable
{
    /// <summary>Computes per-alternative scores for the given problem.</summary>
    protected abstract IEnumerable<T> Execute(McdaProblem<T> data);

    /// <inheritdoc/>
    public Result<Ranking<T>> Run(
        McdaProblem<T> data,
        McdaExecutionOptions? options = null)
    {
        options ??= new McdaExecutionOptions();
        var executionOutput = Execute(data);
        return Result.Success(executionOutput!.CreateRanking(options.RankingOptions));
    }
}
