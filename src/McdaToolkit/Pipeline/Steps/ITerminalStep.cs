using System.Numerics;
using LightResults;
using McdaToolkit.Models.Mcda;

namespace McdaToolkit.Pipeline.Steps;

/// <summary>
/// Represent a final step - pipeline execution, that kind of type should be in pipeline only ones
/// </summary>
public interface ITerminalStep<T> : IPipelineStep<T> 
    where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>Executes the terminal step and returns the solved MCDA result.</summary>
    Result<McdaSolved<T>> Execute(McdaProblem<T> problem);
}