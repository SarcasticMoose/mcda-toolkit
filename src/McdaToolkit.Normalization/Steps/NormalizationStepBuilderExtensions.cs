using System.Numerics;
using McdaToolkit.Pipeline;

namespace McdaToolkit.Normalization.Steps;

/// <summary>
/// Provides extension methods for adding normalization steps to a pipeline.
/// </summary>
public static class NormalizationStepBuilderExtensions
{
    /// <summary>
    /// Adds a normalization step to the pipeline using the specified configuration.
    /// </summary>
    public static AppliedDataPipelineBuilder<T> AddNormalizationStep<T>(
        this AppliedDataPipelineBuilder<T> builder,
        Action<NormalizationStepBuilder<T>> configure)
        where T : struct, IFloatingPointIeee754<T>
    {
        var normalizationBuilder = NormalizationStepBuilder<T>.Create();
        configure(normalizationBuilder);
        builder.AddProcessingStep(normalizationBuilder.Build());
        return builder;
    }
}
