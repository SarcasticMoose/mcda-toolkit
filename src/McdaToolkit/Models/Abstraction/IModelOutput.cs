using McdaToolkit.Models.Rankings;

namespace McdaToolkit.Models.Abstraction;

/// <summary>
/// Strong typed Mcda Model Output
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IModelOutput<T> : IModelOutput
    where T : struct, IEquatable<T>, IComparable<T>
{
    /// <summary>
    /// Ranking executed by model
    /// </summary>
    Ranking<T> Ranking { get; }
}

/// <summary>
/// Marker interface for Mcda Model Output
/// </summary>
public interface IModelOutput { }