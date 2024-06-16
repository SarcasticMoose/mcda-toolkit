using FluentAssertions;
using MathNet.Numerics;
using McdaToolkit.McdaMethods;
using McdaToolkit.McdaMethods.Errors;
using McdaToolkit.UnitTests.Helpers;
using Xunit.Abstractions;

namespace McdaToolkit.UnitTests;

public class McdaMethodsTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public McdaMethodsTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

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
        double[] weights = new double[]
        {
            0.4,0.25,0.35
        };
        int[] types = new int[]
        {
            -1,
            -1,
            1
        };
        double[] expectedTopsisScore = new double[]
        {
            0.38805147,0.76189759,0.58509479,0.06374247,0.97647059,0.43681786
        };

        var topsis = new TopsisMethod();
        var topsisResult = topsis.Calculate(matrix,weights,types);

        var final = topsisResult.Value;
        
        final.Enumerate()
            .Select(x => x.Round(8))
            .Should()
            .BeEquivalentTo(expectedTopsisScore);
    }

    [Fact]
    public void Calculate_WeightAreNotEqualOne_ShouldReturnResultFail()
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
        double[] weights = new double[]
        {
            0.8,0.25,0.35
        };
        int[] types = new int[]
        {
            -1,
            -1,
            1
        };
        
        var topsis = new TopsisMethod();
        var topsisResult = topsis.Calculate(matrix, weights, types);

        topsisResult.IsSuccess.Should().BeFalse();
        topsisResult.HasError<WeightNotSumToOneError>();
    }
    
    [Fact]
    public void Calculate_DecisionCriteriaAreNotBetweenMinusOneAndOne_ShouldReturnResultFail()
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
        double[] weights = new double[]
        {
            0.4,0.25,0.35
        };
        int[] types = new int[]
        {
            -1,
            -2,
            1
        };
        
        var topsis = new TopsisMethod();
        var topsisResult = topsis.Calculate(matrix, weights, types);

        topsisResult.IsSuccess.Should().BeFalse();
        topsisResult.HasError<CriteriaNotBetweenMinusOneAndOne>();
    }
    
    [Fact]
    public void Calculate_DimensionsOfAllMatrixesNotTheSame_ShouldReturnResultFail()
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
        double[] weights = new double[]
        {
            0.4,0.25,0.15,0.20
        };
        int[] types = new int[]
        {
            -1,
            -1,
            1,
            1
        };
        
        var topsis = new TopsisMethod();
        var topsisResult = topsis.Calculate(matrix, weights, types);

        topsisResult.IsSuccess.Should().BeFalse();
        topsisResult.HasError<ArraySizesAreNotEqual>();
    }
}