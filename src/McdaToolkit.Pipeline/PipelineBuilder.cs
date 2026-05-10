using System.Numerics;
using McdaToolkit.Builders;
using McdaToolkit.Validation;

namespace McdaToolkit.Pipeline;

/// <summary>Entry point for building an MCDA processing pipeline. Call <see cref="WithData"/> to proceed to the data configuration phase.</summary>
public class PipelineBuilder<T> where T : struct, IFloatingPointIeee754<T>
{
    private readonly McdaExecutionOptions _options = new();
    private readonly IMcdaInputValidation? _validation;

    public PipelineBuilder(IMcdaInputValidation? validation = null)
    {
        _validation = validation;
    }

    /// <summary>Configures execution options for the pipeline.</summary>
    public PipelineBuilder<T> ConfigureExecution(Action<McdaExecutionOptions> configure)
    {
        configure(_options);
        return this;
    }

    /// <summary>Sets the decision matrix and criteria, transitioning to the processing configuration phase.</summary>
    public ConfiguredPipelineBuilder<T> WithData(Action<McdaProblemBuilder<T>> configure)
    {
        var builder = new McdaProblemBuilder<T>();
        configure(builder);
        var problem = builder.Build();
        return new ConfiguredPipelineBuilder<T>(problem, _options, _validation);
    }
}
