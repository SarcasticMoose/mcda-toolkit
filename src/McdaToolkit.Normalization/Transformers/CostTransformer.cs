using System.Numerics;
using McdaToolkit.Normalization.Transformers.Abstraction;
namespace McdaToolkit.Normalization.Transformers;

internal class CostTransformer<T> : ICriterionTransformer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public MathNet.Numerics.LinearAlgebra.Vector<T> Transform(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var max = data.Maximum();
        return data.Map(x => max - x);
    }
}
