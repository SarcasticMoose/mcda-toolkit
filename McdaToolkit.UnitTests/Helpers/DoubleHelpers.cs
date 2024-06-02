using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.UnitTests.Helpers;

public abstract class TestHelpers
{
    public static bool IsResultTheSame(Vector<double> vector,double[] expected)
    {
        var epsilon = 1.11e-16;
        var result = vector
            .Enumerate()
            .Zip(expected, (r,e) =>
                new {result = r, expected = e});

        foreach (var item in result)
        {
            if (Math.Abs(item.expected - item.result) > epsilon)
            {
                return false;
            }
        }
        return true;
    }
}