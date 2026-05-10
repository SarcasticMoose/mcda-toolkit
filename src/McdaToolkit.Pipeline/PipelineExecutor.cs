using System.Numerics;
using LightResults;
using McdaToolkit.Abstractions;
using McdaToolkit.Pipeline.Steps;
using McdaToolkit.Ranking;

namespace McdaToolkit.Pipeline;

/// <summary>Executes a pre-built MCDA processing pipeline against a fixed decision problem.</summary>
public class PipelineExecutor<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly IReadOnlyList<IProcessingStep<T>> _preProcessingSteps;
    private readonly McdaProblem<T> _problem;
    private readonly McdaExecutionOptions _options;

    internal PipelineExecutor(
        IReadOnlyList<IProcessingStep<T>> steps,
        McdaProblem<T> problem,
        McdaExecutionOptions options)
    {
        _preProcessingSteps = steps;
        _problem = problem;
        _options = options;
    }

    /// <summary>Runs all pre-processing steps then executes the given method, returning a ranked result.</summary>
    public Result<McdaSolved<T>> Execute(IMcdaMethod<T> method)
    {
        var current = _problem;

        foreach (var step in _preProcessingSteps)
        {
            var result = step.Process(current);
            if (!result.IsSuccess(out var processed))
                return result.AsFailure<McdaSolved<T>>();
            current = processed;
        }

        var scores = method.Execute(current);
        var ranking = scores.CreateRanking(_options.RankingOptions);
        return Result.Success(new McdaSolved<T> { Ranking = ranking });
    }
}
