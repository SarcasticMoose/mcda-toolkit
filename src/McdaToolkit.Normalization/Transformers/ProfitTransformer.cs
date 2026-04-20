using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Transformers.Abstraction;

namespace McdaToolkit.Normalization.Transformers;

internal class ProfitTransformer<T> : ICriterionTransformer<T>
    where T : struct, IEquatable<T>, IFormattable
{
    public Vector<T> Transform(Vector<T> data) => data;
}