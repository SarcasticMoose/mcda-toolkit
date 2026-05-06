using System.Numerics;
using McdaToolkit.Models.Mcda;

namespace McdaToolkit.Extensions;

/// <summary>Extension methods for collections of <see cref="CriterionDefinition{T}"/>.</summary>
public static class CriteriaDecisionExtensions
{
    /// <summary>Converts criterion types to a numeric vector (+1 / -1 per type).</summary>
    public static MathNet.Numerics.LinearAlgebra.Vector<T> ToVector<T>(this IEnumerable<CriterionDefinition<T>> criterias)
        where T : struct, IEquatable<T>, IFormattable, IFloatingPoint<T>
    {
        return MathNet.Numerics.LinearAlgebra.Vector<T>.Build.DenseOfEnumerable(criterias.Select(x =>
            T.CreateChecked((int)(object)x.Type)));
    }
}