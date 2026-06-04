using System.Numerics;
using LightResults;
using McdaToolkit.Core;
using McdaToolkit.Pipeline.Steps;
using McdaToolkit.Validation;

namespace McdaToolkit.Pipeline;

/// <summary>
/// Pipeline builder phase unlocked after data is set. Allows adding processing steps and building the executor.
/// </summary>
public class ConfiguredPipelineBuilder<T> where T : struct, IFloatingPointIeee754<T>
{
    private readonly List<IProcessingStep<T>> _steps = [];
    private readonly McdaExecutionOptions _options;
    private readonly IMcdaInputValidation? _validation;
    private readonly McdaProblem<T> _problem;

    internal ConfiguredPipelineBuilder(
        McdaProblem<T> problem,
        McdaExecutionOptions options,
        IMcdaInputValidation? validation)
    {
        _problem = problem;
        _options = options;
        _validation = validation;
    }

    /// <summary>Appends a processing step to the pipeline.</summary>
    public ConfiguredPipelineBuilder<T> AddProcessingStep(IProcessingStep<T> step)
    {
        _steps.Add(step);
        return this;
    }

    /// <summary>Validates inputs and constructs the pipeline executor.</summary>
    public Result<PipelineExecutor<T>> Build()
    {
        if (_validation is not null)
        {
            var validation = _validation.Validate(_problem.Data?.ToArray(), _problem.Criteria?.ToList());
            if (!validation.IsSuccess())
                return Result.Failure<PipelineExecutor<T>>(validation.Errors);
        }

        return Result.Success(new PipelineExecutor<T>(_steps, _problem, _options));
    }
}
