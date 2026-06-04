using System.Numerics;

namespace McdaToolkit.Normalization.Transformers.Abstraction;

/// <summary>Applies a cost/benefit transformation to a criterion vector before normalization.</summary>
public interface ICriterionTransformer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    /// <summary>Transforms the criterion vector according to its type.</summary>
    MathNet.Numerics.LinearAlgebra.Vector<T> Transform(MathNet.Numerics.LinearAlgebra.Vector<T> data);
}
