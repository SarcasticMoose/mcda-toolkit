using System.Numerics;
using LightResults;
using McdaToolkit.Core;
using McdaToolkit.Core.Abstractions;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Pipeline;

/// <summary>Executes a pre-built MCDA processing pipeline against a fixed decision problem.</summary>
public class PipelineExecutor<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly IReadOnlyList<IPreProcessingStep<T>> _preProcessingSteps;
    private readonly McdaProblem<T> _problem;

    internal PipelineExecutor(
        IReadOnlyList<IPreProcessingStep<T>> steps,
        McdaProblem<T> problem)
    {
        _preProcessingSteps = steps;
        _problem = problem;
    }

    /// <summary>Runs all pre-processing steps then executes the given method, returning a ranked result.</summary>
    public Result<McdaSolved<T, TMcdaResult>> Execute<TMcdaResult>(
        IMcdaMethod<T, TMcdaResult> method,
        ExecutionOptions<T> options)
        where TMcdaResult : IMcdaResult<T>
    {
        var current = _problem;

        foreach (var step in _preProcessingSteps)
        {
            var result = step.Execute(current);
            if (!result.IsSuccess(out var processed))
                return result.AsFailure<McdaSolved<T, TMcdaResult>>();
            current = processed;
        }
        var methodResult  = method.Execute(current);
        return new McdaSolved<T, TMcdaResult>()
        {
            Soltution = methodResult
        };
    }
}
