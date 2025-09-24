using LightResults;
using McdaToolkit.Data;

namespace McdaToolkit.Models.Abstraction;

public interface IMcdaMethod<T>
    where T : struct, IEquatable<T>, IComparable<T>
{
    IResult<Ranking.Ranking<T>> Run(McdaInputData data, McdaExecutionOptions? options = null);
}
