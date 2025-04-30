using FluentAssertions;
using MathNet.Numerics;
using McdaToolkit.Mcda.Methods.Factories;
using McdaToolkit.Mcda.Methods.Promethee2;
using McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions.Factory;
using McdaToolkit.Mcda.Methods.Topsis;
using McdaToolkit.Mcda.Methods.Vikor;
using McdaToolkit.Normalization.Enums;

namespace McdaToolkit.UnitTests;

public class McdaMethodsTests
{
    [Fact]
    public void Calculate_TopsisMethod_ShouldBeEqualToExpected()
    {
        var matrix = new[,]
        {
            {690, 3.1, 9, 7, 4},
            {590, 3.9, 7, 6, 10},
            {600, 3.6, 8, 8, 7},
            {620, 3.8, 7, 10, 6},
            {700, 2.8, 10, 4, 6},
            {650, 4.0, 6, 9, 8}
        };
        double[] weights = [0.3, 0.2, 0.2, 0.15, 0.15];
        int[] types = [1,1,1,1,1];
        double[] expectedTopsisScore = [0.416704, 0.551900, 0.539620, 0.539926, 0.429128, 0.568142];
        
        var data = new DefaultDataProviderBuilder()
            .AddWeights(weights)
            .AddDecisionCriteria(types)
            .AddDecisionMatrix(matrix)
            .Build();
        
        var topsis = MethodFactory.CreateTopsis(new TopsisOptions()
        {
            NormalizationMethod = NormalizationMethod.Vector
        });
        
        var topsisResult = topsis.Run(data);
        
        topsisResult
            .Value.V
            .Select(x => x.Round(6))
            .Should()
            .BeEquivalentTo(expectedTopsisScore);
    }
    
    [Fact]
    public void Calculate_VikorMethod_ShouldBeEqualToExpected()
    {
        var matrix = new[,]
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

        var data = new DefaultDataProviderBuilder()
            .AddWeights(weights)
            .AddDecisionCriteria(types)
            .AddDecisionMatrix(matrix)
            .Build();

        var vikorResult = MethodFactory
            .CreateVikor(new VikorOptions())
            .Run(data);
        
        vikorResult
            .Value.Q
            .Enumerate()
            .Select(x => x.Round(3))
            .Should()
            .BeEquivalentTo(expectedVikorScore);
    }
    
    [Fact]
    public void Calculate_Promethee2Method_ShouldBeEqualToExpected()
    {
        var expectedRanking = new List<Promethee2ScoreRanking>()
        {
            new()
            {
                Alternative = 1,
                Rank = 1,
                Score = 0.371
            },
            new()
            {
                Alternative = 2,
                Rank = 3,
                Score = -0.250
            },
            new()
            {
                Alternative = 3,
                Rank = 4,
                Score = -0.309
            },
            new()
            {
                Alternative = 4,
                Rank = 2,
                Score = 0.188
            }
        };
        double[,] matrix = new double[,]
        {
            { 1900, 0.83, 24, 3.5, 8.5, 3, 4, 30, 4, 1 },
            { 2200, 0.77, 22, 3,   7.5, 1, 5, 14, 3, 1 },
            { 1300, 0.67, 20, 3,   5,   0, 4, 7,  3, 1 },
            { 1800, 0.74, 20, 4,  10,   0, 5, 14, 4, 3 }
        };
        double[] weights = [   
            0.2122,
            0.1678, 
            0.0633, 
            0.0345, 
            0.1123, 
            0.0798, 
            0.0112, 
            0.0860, 
            0.1584, 
            0.0745];
        int[] types = [-1, 1, 1, 1, 1, 1, 1, 1, 1, 1];

        var data = new DefaultDataProviderBuilder()
            .AddWeights(weights)
            .AddDecisionCriteria(types)
            .AddDecisionMatrix(matrix)
            .Build();

        var promethee2Result = MethodFactory
            .CreatePromethee2(new Promethee2Options()
            {
                NormalizationMethod = NormalizationMethod.MinMax,
                PreferenceFunction = PreferenceFunction.Unnamed
            })
            .Run(data)
            .Value
            .Ranking
            .Select(x => (x with { Score = x.Score.Round(3) }))
            .Should()
            .BeEquivalentTo(expectedRanking);
    }
}