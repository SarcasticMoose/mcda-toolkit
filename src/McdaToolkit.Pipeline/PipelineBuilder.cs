using System.Numerics;
using LightResults;
using McdaToolkit.Builders;
using McdaToolkit.Pipeline.Steps;
using McdaToolkit.Validation;

namespace McdaToolkit.Pipeline;

/// <summary>Builds a sequential processing pipeline of MCDA pre-processing steps.</summary>
public class PipelineBuilder<T> where T : struct, IFloatingPointIeee754<T>
{
    private readonly List<IProcessingStep<T>> _steps = new();
    private readonly McdaExecutionOptions _options = new();
    private readonly IMcdaInputValidation _validation;
    private McdaProblem<T>? _problem;

    public PipelineBuilder(IMcdaInputValidation validation = null)
    {
        _validation = validation;
    }

    /// <summary>Sets the decision matrix and criteria for this pipeline.</summary>
    public PipelineBuilder<T> WithData(Action<McdaProblemBuilder<T>> configure)
    {
        var builder = new McdaProblemBuilder<T>();
        configure(builder);
        _problem = builder.Build();
        return this;
    }

    /// <summary>Appends a pre-processing step to the pipeline.</summary>
    public PipelineBuilder<T> AddProcessingStep(IProcessingStep<T> step)
    {
        _steps.Add(step);
        return this;
    }

    /// <summary>Configures execution options for this pipeline.</summary>
    public PipelineBuilder<T> ConfigureExecution(Action<McdaExecutionOptions> configure)
    {
        configure(_options);
        return this;
    }

    /// <summary>Constructs and validates the pipeline executor.</summary>
    public Result<PipelineExecutor<T>> Build()
    {
        if (_problem is null)
            return Result.Failure<PipelineExecutor<T>>("Data must be set before building the pipeline. Call WithData first.");

        var validation = _validation.Validate(_problem.Data?.ToArray(), _problem.Criteria?.ToList());
        if (!validation.IsSuccess())
            return Result.Failure<PipelineExecutor<T>>(validation.Errors);

        return Result.Success(new PipelineExecutor<T>(_steps, _problem, _options));
    }
}
