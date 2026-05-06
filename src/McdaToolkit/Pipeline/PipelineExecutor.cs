using System.Numerics;
using LightResults;
using McdaToolkit.Models.Mcda;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Pipeline;

/// <summary>Executes a pre-built MCDA processing pipeline.</summary>
public class PipelineExecutor<T>
    where T : struct, IFloatingPointIeee754<T>
{
    private readonly IReadOnlyList<IPreProcessingStep<T>> _preProcessingSteps;
    private readonly ITerminalStep<T> _terminalStep;

    internal PipelineExecutor(IReadOnlyList<IPipelineStep<T>> steps)
    {
        _preProcessingSteps = steps.OfType<IPreProcessingStep<T>>().ToList();
        _terminalStep = steps.OfType<ITerminalStep<T>>().Single();
    }

    /// <summary>Executes the pipeline by running all pre-processing steps in order, followed by the terminal method step.</summary>
    public Result<McdaSolved<T>> Execute(McdaProblem<T> problem)
    {
        var current = problem;

        foreach (var step in _preProcessingSteps)
        {
            var result = step.Process(current);
            if (!result.IsSuccess(out var processed))
                return result.AsFailure<McdaSolved<T>>();
            current = processed;
        }

        return _terminalStep.Execute(current);
    }
}