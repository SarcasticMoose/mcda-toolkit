using System.Numerics;
using MathNet.Numerics;
using McdaMethods.Normalization.NormalizationMethods.Abstraction;
using static System.Double;


namespace McdaMethods.Normalization.NormalizationMethods;

public class MinMaxNormalization<TValue> : INormalizationMethod<TValue>
where TValue : struct, INumber<TValue>
{
    public MathNet.Numerics.LinearAlgebra.Vector<TValue> Normalize<T>(MathNet.Numerics.LinearAlgebra.Vector<TValue> data, bool cost = false)
    {
        if (data.Maximum() == data.Minimum())
        {
            return MathNet.Numerics.LinearAlgebra.Vector<TValue>.Build.Dense(data.Count, (i) => (TValue)Convert.ChangeType(1,typeof(TValue)));
        }

        TValue difference = data.Maximum() - data.Minimum();
        
        if (cost)
        {
            return (data.Maximum() - data) / difference;
        }
        return (data - data.Minimum()) / difference;
    }
}