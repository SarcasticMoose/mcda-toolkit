using FluentAssertions;
using MathNet.Numerics;
using McdaToolkit.Mcda;
using McdaToolkit.Mcda.Factories;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Mcda.Methods.Vikor;

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
    public void Calculate_VikorMethod_ShouldBeEqualToExpecteddsadas()
    {
        var matrix = new double[,]
        {
            { 78, 56, 34, 6 },
            { 4, 45, 3, 97 },
            { 18, 2, 50, 63 },
            { 9, 14, 11, 92 },
            { 85, 9, 100, 29 },
        };
        double[] weights = [0.25, 0.25, 0.25, 0.25];
        int[] types = [1, 1, 1, 1];
        double[] expectedVikorScore = [0.5679, 0.7667, 1, 0.7493, 0];

        var dataProvider = DefaultDataProviderFactory.CreateDataProvider();
        dataProvider.ProvideData(matrix, weights, types, VikorParameters.CreateDefault());
        var vikor = MethodFactory.CreateVikor(new McdaMethodOptions());
        var vikorResult = vikor.Run(dataProvider);
        
        vikorResult.Value.Q.Enumerate()
            .Select(x => x.Round(4))
            .Should()
            .BeEquivalentTo(expectedVikorScore);
    }
}