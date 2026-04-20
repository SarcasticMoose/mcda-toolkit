using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Normalization;

public record McdaProblem<T> where T : struct, IEquatable<T>, IFormattable
{
    public Matrix<T> Data { get; init; }
    public CriterionDefinition<T>[] Criteria { get; init; }
}