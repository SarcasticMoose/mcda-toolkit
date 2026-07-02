using System.Numerics;
using LightResults;
using McdaToolkit.Core;
using McdaToolkit.Pipeline.Steps;
using McdaToolkit.Validation;

namespace McdaToolkit.Pipeline;

/// <summary>
/// Represents the pipeline builder stage that becomes available after
/// a decision problem has been configured with input data.
/// Allows registering processing steps and creating a validated
/// <see cref="PipelineExecutor{T}"/> instance.
/// </summary>
/// <typeparam name="T">
/// Floating-point numeric type used by the decision problem.
/// </typeparam>
public class AppliedDataPipelineBuilder<T> where T : struct, IFloatingPointIeee754<T>
{
    private readonly List<IPreProcessingStep<T>> _steps = [];
    private readonly IMcdaInputValidation? _validation;
    private readonly McdaProblem<T> _problem;

    internal AppliedDataPipelineBuilder(
        McdaProblem<T> problem,
        IMcdaInputValidation? validation)
    {
        _problem = problem;
        _validation = validation;
    }

    /// <summary>
    /// Adds a processing step to the execution pipeline.
    /// </summary>
    /// <param name="step">
    /// The processing step to append.
    /// </param>
    /// <returns>
    /// The current builder instance, allowing further pipeline configuration.
    /// </returns>
    internal AppliedDataPipelineBuilder<T> AddProcessingStep(IPreProcessingStep<T> step)
    {
        _steps.Add(step);
        return this;
    }

    /// <summary>
    /// Validates the configured decision problem and creates
    /// a <see cref="PipelineExecutor{T}"/> instance.
    /// </summary>
    /// <returns>
    /// A successful result containing the constructed pipeline executor,
    /// or a failure result containing validation errors.
    /// </returns>
    public Result<PipelineExecutor<T>> Build()
    {
        if (_validation is not null)
        {
            var validation = _validation.Validate(_problem.Data?.ToArray(), _problem.Criteria?.ToList());
            if (!validation.IsSuccess())
                return Result.Failure<PipelineExecutor<T>>(validation.Errors);
        }

        return Result.Success(new PipelineExecutor<T>(_steps, _problem));
    }
}
