using MathNet.Numerics.LinearAlgebra;
using static System.Double;


namespace McdaMethods.NormalizationMethods;

public class MinMaxNormalization : INormalizationMethod
{
    public Vector<double> Normalize<T>(Vector<double> data, bool cost = false)
    {
        double difference = data.Maximum() - data.Minimum();

        if (Math.Abs(difference) < Epsilon)
        {
            return Vector<double>.Build.Dense(data.Count, (i) => 1);
        }

        if (cost)
        {
            return (data.Maximum() - data) / difference;
        }

        return (data - data.Minimum()) / difference;
    }
}