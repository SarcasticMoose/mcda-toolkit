using FluentAssertions;
using MathNet.Numerics;
using McdaToolkit.Mcda;
using McdaToolkit.Mcda.Factories;
using McdaToolkit.Mcda.Methods.Vikor;
using McdaToolkit.Normalization.Enums;

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
        double[] expectedTopsisScore = [0.3881,0.7619,0.5851,0.0637,0.9765,0.4368];
        
        var dataProvider = DefaultDataProviderFactory.CreateDataProvider();
        dataProvider.ProvideData(matrix, weights, types);
        var topsis = MethodFactory.CreateTopsis(new McdaMethodOptions());
        var topsisResult = topsis.Run(dataProvider);
        
        topsisResult.Value.Score
            .Select(x => x.Round(4))
            .Should()
            .BeEquivalentTo(expectedTopsisScore);
    }
    
    [Fact]
    public void Calculate_VikorMethod_ShouldBeEqualToExpected()
    {
        var matrix = new double[,]
        {
            {3, 6, 4, 20, 2, 30000 },
            {4, 4, 6, 15, 2.2, 32000 },
            {6, 5, 9, 18, 3, 32100 },
            {5, 6, 3, 23, 2.8, 28000 },
            {4, 8, 7, 30, 1.5, 29000 },
            {8, 3, 6, 35, 1.9, 27000 },
            {7, 2, 5, 33, 1.7, 28500 },
            {3, 8, 3, 34, 1.6, 30500 },
            {8, 4, 8, 40, 2.5, 33000 },
            {9, 3, 7, 34, 2, 29800 }
        };
        double[] weights = [0.1,0.2,0.1,0.2,0.1,0.3];
        int[] types = [1,1,1,-1,-1,-1];
        double[] expectedVikorScore = [0.297,0.661,0.630,0.123,0.050,0.272,0.497,0.436,1.0,0.404];

        var dataProvider = DefaultDataProviderFactory.CreateDataProvider();
        var provideResult = dataProvider.ProvideData(matrix, weights, types, VikorParameters.CreateDefault());
        var vikor = MethodFactory.CreateVikor(new McdaMethodOptions()
        {
            NormalizationMethod = NormalizationMethod.Vector
        });
        var vikorResult = vikor.Run(dataProvider);
        
        vikorResult.Value.Q.Enumerate()
            .Select(x => x.Round(3))
            .Should()
            .BeEquivalentTo(expectedVikorScore);
    }
}