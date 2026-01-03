using LightResults;
using McdaToolkit.Data;

namespace McdaToolkit.Models.Abstraction;

/// <summary>
/// Represents a Multi-Criteria Decision Analysis (MCDA) method for a specific numeric type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">
/// The numeric type used for criteria evaluations. Must be a value type implementing 
/// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.
/// </typeparam>
public interface IMcdaMethod<T>
    where T : struct, IEquatable<T>, IComparable<T>
{
    /// <summary>
    /// Gets the name of the MCDA method.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Executes the MCDA method on the provided input data.
    /// </summary>
    /// <param name="data">The input data containing alternatives, criteria, and evaluations.</param>
    /// <param name="options">Optional execution options that can modify how the method runs.</param>
    /// <returns>
    /// An <see cref="IResult{TResult}"/> containing the execution details, including the ranking
    /// of alternatives and any additional diagnostic information.
    /// </returns>
    IResult<ExecutionDetails<T>> Run(McdaInputData data, McdaExecutionOptions? options = null);
}