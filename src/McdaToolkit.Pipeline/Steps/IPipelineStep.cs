using System.Numerics;

namespace McdaToolkit.Pipeline.Steps;

/// <summary>Marker interface for all pipeline steps operating on floating-point MCDA data.</summary>
public interface IPipelineStep<T> where T : struct, IFloatingPointIeee754<T>;