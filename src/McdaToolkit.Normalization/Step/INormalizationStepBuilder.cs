using System.Numerics;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Normalization.Step;

/// <summary>Configures and builds a normalization step for an MCDA pipeline.</summary>
public interface INormalizationStepBuilder<T>
    where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>Selects the normalization method to apply.</summary>
    INormalizationStepBuilder<T> WithMethod(NormalizationMethod method);
    IPreProcessingStep<T> Build();
}