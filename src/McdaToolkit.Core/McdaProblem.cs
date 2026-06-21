using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Core;

/// <summary>Represents an MCDA problem: a decision matrix paired with criterion definitions.</summary>
public record McdaProblem<T>
    where T : struct, IFloatingPointIeee754<T>
{
    internal McdaProblem(Matrix<T> data, CriterionDefinition<T>[] criterias)
    {
        Data = data;
        Criteria = criterias;
    }

    /// <summary>Decision matrix where rows are alternatives and columns are criteria.</summary>
    public Matrix<T> Data { get; internal set; }

    /// <summary>Criteria definitions including type and weight for each column.</summary>
    public CriterionDefinition<T>[] Criteria { get; internal set; }

    internal static McdaProblem<T> Create(Matrix<T> data, CriterionDefinition<T>[] criterias) => new(data, criterias);
}
