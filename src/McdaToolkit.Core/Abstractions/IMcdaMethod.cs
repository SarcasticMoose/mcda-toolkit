using System.Numerics;

namespace McdaToolkit.Core.Abstractions;

/// <summary>
/// Defines a contract for Multi-Criteria Decision Analysis (MCDA) methods
/// that evaluate alternatives and produce a result containing their scores
/// or preference values.
/// </summary>
/// <typeparam name="TInput">
/// Numeric type used for criterion values and calculations.
/// </typeparam>
/// <typeparam name="TOutput">
/// Type of result returned by the MCDA method.
/// </typeparam>
public interface IMcdaMethod<TInput, TOutput>
    where TInput : struct, IFloatingPointIeee754<TInput>
    where TOutput : IMcdaResult<TInput>
{
    internal TOutput Execute(McdaProblem<TInput> problem);
}
