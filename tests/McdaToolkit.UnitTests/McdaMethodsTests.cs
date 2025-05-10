using FluentAssertions;
using MathNet.Numerics;
using McdaToolkit.Methods.Promethee2;
using McdaToolkit.Methods.Promethee2.PreferenceFunctions.Factory;
using McdaToolkit.Methods.Topsis;
using McdaToolkit.Methods.Vikor;
using McdaToolkit.Normalization.Enums;
using McdaToolkit.Shared.Factories;
using McdaToolkit.Shared.Providers;
using McdaToolkit.Shared.Ranking;

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
        var expectedTopsisScore = new Ranking<double>()
        {
            RankingItems = new List<RankingRow<double>>()
            {
                new RankingRow<double>
                {
                    Alternative = 1,
                    Rank = 6,
                    Score = 0.417
                },
                new RankingRow<double>
                {
                    Alternative = 2,
                    Rank = 2,
                    Score = 0.552
                },
                new RankingRow<double>
                {
                    Alternative = 3,
                    Rank = 4,
                    Score = 0.54
                },
                new RankingRow<double>
                {
                    Alternative = 4,
                    Rank = 3,
                    Score = 0.54
                },
                new RankingRow<double>
                {
                    Alternative = 5,
                    Rank = 5,
                    Score = 0.429
                },
                new RankingRow<double>
                {
                    Alternative = 6,
                    Rank = 1,
                    Score = 0.568
                }
            }
        };
        
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
            .Value
            .Select(x => x with
            {
                Score = x.Score.Round(3)
            })
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
        var expectedVikorScore = new Ranking<VikorScore>()
        {
            RankingItems = new List<RankingRow<VikorScore>>
            {
                new() { Alternative = 1, Rank = 7, Score = new() { Q = 0.297, R = 0.15, S = 0.473 } },
                new() { Alternative = 2, Rank = 2, Score = new() { Q = 0.661, R = 0.25, S = 0.563 } },
                new() { Alternative = 3, Rank = 3, Score = new() { Q = 0.63, R = 0.255, S = 0.529 } },
                new() { Alternative = 4, Rank = 9, Score = new() { Q = 0.123, R = 0.1, S = 0.434 } },
                new() { Alternative = 5, Rank = 10, Score = new() { Q = 0.05, R = 0.12, S = 0.337 } },
                new() { Alternative = 6, Rank = 8, Score = new() { Q = 0.272, R = 0.167, S = 0.42 } },
                new() { Alternative = 7, Rank = 4, Score = new() { Q = 0.497, R = 0.2, S = 0.532 } },
                new() { Alternative = 8, Rank = 5, Score = new() { Q = 0.436, R = 0.175, S = 0.534 } },
                new() { Alternative = 9, Rank = 1, Score = new() { Q = 1.0, R = 0.3, S = 0.733 } },
                new() { Alternative = 10, Rank = 6, Score = new() { Q = 0.404, R = 0.167, S = 0.525 } }
            }
        };

        var data = new DefaultDataProviderBuilder()
            .AddWeights(weights)
            .AddDecisionCriteria(types)
            .AddDecisionMatrix(matrix)
            .Build();

        var vikorResult = MethodFactory
            .CreateVikor(new VikorOptions())
            .Run(data);
        
        vikorResult
            .Value
            .Select(x => x with 
            { 
                Score = new VikorScore
                {
                    Q = x.Score.Q.Round(3),
                    R = x.Score.R.Round(3),
                    S = x.Score.S.Round(3)
                }
            })
            .Should()
            .BeEquivalentTo(expectedVikorScore);
    }
    
    [Fact]
    public void Calculate_Promethee2Method_ShouldBeEqualToExpected()
    {
        var expectedRanking = new Ranking<double>()
        {
            RankingItems = new List<RankingRow<double>>()
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

        var options = new Promethee2Options()
            {
                NormalizationMethod = NormalizationMethod.MinMax
            };
        options.PreferenceFunction = PreferenceFunction.Unnamed;
        var promethee2Result = MethodFactory
            .CreatePromethee2(options)
            .Run(data)
            .Value
            .Select(x => (x with { Score = x.Score.Round(3) }))
            .Should()
            .BeEquivalentTo(expectedRanking.RankingItems);
    }
}