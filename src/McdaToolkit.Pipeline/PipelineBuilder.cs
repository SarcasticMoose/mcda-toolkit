using System.Numerics;
using McdaToolkit.Core.Builders;
using McdaToolkit.Validation;

namespace McdaToolkit.Pipeline;

/// <summary>Entry point for building an MCDA processing pipeline. Call <see cref="ApplyData"/> to proceed to the data configuration phase.</summary>
public class PipelineBuilder<T> where T : struct, IFloatingPointIeee754<T>
{
    private readonly IMcdaInputValidation _validation = new McdaInputValidation();

    /// <summary>Sets the decision matrix and criteria, transitioning to the processing configuration phase.</summary>
    public AppliedDataPipelineBuilder<T> ApplyData(Action<McdaProblemBuilder<T>> configure)
    {
        var builder = new McdaProblemBuilder<T>();
        configure(builder);
        var problem = builder.Build();
        return new AppliedDataPipelineBuilder<T>(problem, _validation);
    }
}
