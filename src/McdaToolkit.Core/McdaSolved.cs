
using System.Numerics;
using McdaToolkit.Core.Abstractions;

namespace McdaToolkit.Core;

/// <summary>
/// Represents the result of solving a multi-criteria decision analysis (MCDA) problem.
/// </summary>
/// <typeparam name="TInput">
/// The numeric type used in the calculations. Must implement
/// <see cref="IFloatingPointIeee754{TSelf}"/>.
/// </typeparam>
/// <typeparam name="TResult">
/// The type of the MCDA result.
/// </typeparam>
public readonly record struct McdaSolved<TInput, TResult>
    where TInput : struct, IFloatingPointIeee754<TInput>
    where TResult : IMcdaResult<TInput>
{
    /// <summary>
    /// Gets the solution produced by the MCDA method.
    /// </summary>
    public TResult Soltution { get; init; }
}
