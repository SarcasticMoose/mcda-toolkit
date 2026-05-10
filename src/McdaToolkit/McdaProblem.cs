using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit;

/// <summary>Represents an MCDA problem: a decision matrix paired with criterion definitions.</summary>
public record McdaProblem<T> where T : struct, IEquatable<T>, IFormattable
{
    /// <summary>Decision matrix where rows are alternatives and columns are criteria.</summary>
    public Matrix<T> Data { get; init; }

    /// <summary>Criteria definitions including type and weight for each column.</summary>
    public CriterionDefinition<T>[] Criteria { get; init; }
}