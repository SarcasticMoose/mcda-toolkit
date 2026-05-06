using System.Numerics;
using LightResults;
using McdaToolkit.Models.Mcda;

namespace McdaToolkit.Pipeline.Steps;

/// <summary>Represents a single pre-processing step in an MCDA pipeline.</summary>
public interface IPreProcessingStep<T> : IPipelineStep<T>  
    where T :  struct, IFloatingPointIeee754<T>
{
    /// <summary>Applies the pre-processing transformation to the given problem and returns the modified result.</summary>
    Result<McdaProblem<T>> Process(McdaProblem<T> problem);
}