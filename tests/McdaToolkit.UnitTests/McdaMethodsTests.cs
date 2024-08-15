using FluentAssertions;
using MathNet.Numerics;
using McdaToolkit.Enums;
using McdaToolkit.Mcda.Methods;
using McdaToolkit.Mcda.Options;

namespace McdaToolkit.UnitTests;

public class McdaMethodsTests
{
    [Fact]
    public void Calculate_TopsisMethod_ShouldBeEqualToExpected()
    {
        var matrix = new double[,]
        {
            { 66, 56, 95 },
            { 61, 55, 166 },
            { 65, 49, 113 },
            { 95, 56, 99 },
            { 63, 43, 178 },
            { 74, 59, 140 },
        };
        double[] weights = [0.4,0.25,0.35];
        int[] types = [-1, -1, 1];
        double[] expectedTopsisScore = [0.38805147,0.76189759,0.58509479,0.06374247,0.97647059,0.43681786];

        var topsis = new Topsis(new McdaMethodOptions()
        {
            NormalizationMethod = NormalizationMethod.MinMax
        });
        
        var topsisResult = topsis.Calculate(matrix,weights,types);
        var final = topsisResult.Value;
        
        final.Enumerate()
            .Select(x => x.Round(8))
            .Should()
            .BeEquivalentTo(expectedTopsisScore);
    }
}