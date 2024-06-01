using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaMethods.UnitTests.Helpers;

public abstract class TestHelpers
{
    public static bool IsResultTheSame(Vector<double> vector,double[] expected)
    {
        var result = vector.Enumerate().Zip(expected, (r,e) => new {result = r, expected = e});

        foreach (var item in result)
        {
            if (Math.Abs(item.expected - item.result.Round(3)) < Double.Epsilon)
            {
                return true;
            }
        }
        return false;
    }
}