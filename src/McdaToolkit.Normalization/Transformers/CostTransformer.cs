using System.Numerics;
namespace McdaToolkit.Normalization.Transformers;

internal class CostTransformer<T> : ICriterionTransformer<T>
    where T : struct, IFloatingPointIeee754<T>
{
    public MathNet.Numerics.LinearAlgebra.Vector<T> Transform(MathNet.Numerics.LinearAlgebra.Vector<T> data)
    {
        var max = data.Max();
        return data.Map(x => max - x);
    }
}
