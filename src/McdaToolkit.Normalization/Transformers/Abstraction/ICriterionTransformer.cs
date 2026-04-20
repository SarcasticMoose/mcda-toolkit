using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Normalization.Transformers.Abstraction;

public interface ICriterionTransformer<T> 
    where T : struct, IEquatable<T>, IFormattable
{
    Vector<T> Transform(Vector<T> data);
}