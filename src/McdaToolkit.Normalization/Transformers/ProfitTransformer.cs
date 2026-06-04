using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Transformers.Abstraction;
namespace McdaToolkit.Normalization.Transformers;

internal class ProfitTransformer<T> : ICriterionTransformer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public MathNet.Numerics.LinearAlgebra.Vector<T> Transform(MathNet.Numerics.LinearAlgebra.Vector<T> data) => data;
}
