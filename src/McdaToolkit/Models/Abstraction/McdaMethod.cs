using LightResults;
using McdaToolkit.Data;
using McdaToolkit.Models.Ranking;

namespace McdaToolkit.Models.Abstraction;

public abstract class McdaMethod<T> : IMcdaMethod<T>
    where T : struct, IEquatable<T>, IComparable<T>
{
    public IResult<Ranking<T>> Run(McdaInputData data, McdaExecutionOptions? options = null)
    {
        options ??= new McdaExecutionOptions();
        var executionOutput = Execute(data);
        return Result.Success(executionOutput!.CreateRanking(options.RankingOptions));
    }

    protected abstract IEnumerable<T> Execute(McdaInputData data);
}
