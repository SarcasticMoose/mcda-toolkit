using System.Numerics;
using LightResults;
using McdaToolkit.Models.Abstraction;
using McdaToolkit.Models.Mcda;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Pipeline;

/// <summary>Adapts an <see cref="IMcdaMethod{T}"/> to the <see cref="ITerminalStep{T}"/> interface, serving as the final step in a pipeline.</summary>
internal sealed class McdaMethodAdapter<T> : ITerminalStep<T>
    where T : struct, IFloatingPointIeee754<T>, IComparable<T>
{
    private readonly IMcdaMethod<T> _method;

    public McdaMethodAdapter(IMcdaMethod<T> method) => _method = method;

    public Result<McdaSolved<T>> Execute(McdaProblem<T> problem)
    {
        var runResult = _method.Run(problem);
        if (!runResult.IsSuccess(out var result))
        {
            return runResult.AsFailure<McdaSolved<T>>();
        }
        return new McdaSolved<T>()
        {
            Ranking = result
        };
    }
}