using System.Numerics;

namespace McdaToolkit.Core.Abstractions;

/// <summary>
/// Represents the result of an MCDA (Multi-Criteria Decision Analysis) problem.
/// </summary>
/// <typeparam name="T">The type of the result values.</typeparam>
public interface IMcdaResult<T>
    where T : struct, IFloatingPointIeee754<T>
{
}
