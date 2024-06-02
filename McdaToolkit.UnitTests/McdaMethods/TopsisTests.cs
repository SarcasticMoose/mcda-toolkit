using FluentAssertions;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda;
using McdaToolkit.Options;
using McdaToolkit.UnitTests.Helpers;

namespace McdaToolkit.UnitTests.McdaMethods;

public class TopsisTests
{
    [Fact]
    public void CalculateMatrixOfDouble_ShouldReturn_CorrectExpectedMatrix()
    {
        var matrix = new double[,]
        {
            { 1, 3000 },
            { 2, 3750 },
            { 5, 4500 }
        };
        double[] weights = new double[]
        {
            0.5,0.5
        };
        int[] types = new int[]
        {
            -1,
            1
        };
        double[] expectedTopsisScore = new double[]
        {
            0.5, 0.625, 0.5
        };

        var topsis = new TopsisMethod();
        var result = topsis.Calculate(matrix,weights,types);
        TestHelpers.IsResultTheSame(result,expectedTopsisScore).Should().BeTrue();
    }
}