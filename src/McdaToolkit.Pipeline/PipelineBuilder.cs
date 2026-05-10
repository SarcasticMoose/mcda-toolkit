using System.Numerics;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Pipeline;

/// <summary>Builds a sequential processing pipeline of MCDA pre-processing steps.</summary>
public class PipelineBuilder<T> where T : struct, IFloatingPointIeee754<T>
{
    private readonly List<IPreProcessingStep<T>> _steps = new();
    private readonly McdaExecutionOptions _options = new();

    /// <summary>Appends a pre-processing step to the pipeline.</summary>
    public PipelineBuilder<T> AddPreprocessingStep(IPreProcessingStep<T> step)
    {
        _steps.Add(step);
        return this;
    }

    /// <summary>Configures execution for this pipeline.</summary>
    public PipelineBuilder<T> ConfigureExecution(Action<McdaExecutionOptions> configure)
    {
        configure(_options);
        return this;
    }

    /// <summary>Constructs the pipeline executor from the configured steps and options.</summary>
    public PipelineExecutor<T> Build() => new(_steps, _options);
}
