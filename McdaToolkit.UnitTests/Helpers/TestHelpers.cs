using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.UnitTests.Helpers;

public abstract class TestHelpers
{
    public static bool CheckEquality(Matrix<double> normalizedMatrix,double[][] expected)
    {
        return normalizedMatrix
            .EnumerateRows()
            .Select((x, i) => IsResultTheSame(x, expected[i]))
            .All(y => y);
    }
    private static bool IsResultTheSame(Vector<double> vector,double[] expected)
    {
        var epsilon = 1.11e-16;
        var result = vector
            .Select(x => x.Round(8))
            .Zip(expected, (r,e) => new {result = r, expected = e});

        foreach (var item in result)
        {
            if (Math.Abs(item.expected - item.result) < epsilon)
            {
                return true;
            }
        }
        return false;
    }
}