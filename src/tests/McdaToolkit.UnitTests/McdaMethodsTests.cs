using MathNet.Numerics;
using McdaToolkit.Data.Builders;
using McdaToolkit.Data.Normalization;
using McdaToolkit.Models.Ranking;
using McdaToolkit.Models.School.European.Promethee.II;
using McdaToolkit.Models.School.European.Promethee.Preference.Functions.FShape;
using McdaToolkit.Models.School.European.Topsis;
using McdaToolkit.Models.School.European.Vikor;

namespace McdaToolkit.UnitTests;

public class McdaMethodsTests
{
    [Fact]
    public void Calculate_TopsisMethod_ShouldBeEqualToExpected()
    {
        var matrix = new[,]
        {
            { 690, 3.1, 9, 7, 4 },
            { 590, 3.9, 7, 6, 10 },
            { 600, 3.6, 8, 8, 7 },
            { 620, 3.8, 7, 10, 6 },
            { 700, 2.8, 10, 4, 6 },
            { 650, 4.0, 6, 9, 8 },
        };
        double[] weights = { 0.3, 0.2, 0.2, 0.15, 0.15 };
        int[] types = { 1, 1, 1, 1, 1 };

        var expectedTopsisScore = new Ranking<double>(
            new List<RankingRow<double>>
            {
                new(1, 6, 0.417),
                new(2, 2, 0.552),
                new(3, 4, 0.54),
                new(4, 3, 0.54),
                new(5, 5, 0.429),
                new(6, 1, 0.568),
            }
        );

        var data = DataBuilder
            .Create()
            .AddWeights(weights)
            .AddDecisionCriteria(types)
            .AddDecisionMatrix(matrix)
            .Build();

        var topsis = TopsisBuilder
            .Create()
            .WithNormalizationMethod(NormalizationMethod.Vector)
            .Build();

        var topsisResult = topsis.Run(data);
        topsisResult.IsSuccess(out var value);

        var actual = value!
            .Select(x => new RankingRow<double>(x.Alternative, x.Rank, x.Score.Round(3)))
            .ToList();

        Assert.Equivalent(expectedTopsisScore, new Ranking<double>(actual));
    }

    [Fact]
    public void Calculate_VikorMethod_ShouldBeEqualToExpected()
    {
        var matrix = new[,]
        {
            { 3, 6, 4, 20, 2, 30000 },
            { 4, 4, 6, 15, 2.2, 32000 },
            { 6, 5, 9, 18, 3, 32100 },
            { 5, 6, 3, 23, 2.8, 28000 },
            { 4, 8, 7, 30, 1.5, 29000 },
            { 8, 3, 6, 35, 1.9, 27000 },
            { 7, 2, 5, 33, 1.7, 28500 },
            { 3, 8, 3, 34, 1.6, 30500 },
            { 8, 4, 8, 40, 2.5, 33000 },
            { 9, 3, 7, 34, 2, 29800 },
        };
        double[] weights = { 0.1, 0.2, 0.1, 0.2, 0.1, 0.3 };
        int[] types = { 1, 1, 1, -1, -1, -1 };

        var expectedVikorScore = new Ranking<VikorScore>(
            new List<RankingRow<VikorScore>>
            {
                new(
                    1,
                    7,
                    new VikorScore
                    {
                        Q = 0.297,
                        R = 0.15,
                        S = 0.473,
                    }
                ),
                new(
                    2,
                    2,
                    new VikorScore
                    {
                        Q = 0.661,
                        R = 0.25,
                        S = 0.563,
                    }
                ),
                new(
                    3,
                    3,
                    new VikorScore
                    {
                        Q = 0.63,
                        R = 0.255,
                        S = 0.529,
                    }
                ),
                new(
                    4,
                    9,
                    new VikorScore
                    {
                        Q = 0.123,
                        R = 0.1,
                        S = 0.434,
                    }
                ),
                new(
                    5,
                    10,
                    new VikorScore
                    {
                        Q = 0.05,
                        R = 0.12,
                        S = 0.337,
                    }
                ),
                new(
                    6,
                    8,
                    new VikorScore
                    {
                        Q = 0.272,
                        R = 0.167,
                        S = 0.42,
                    }
                ),
                new(
                    7,
                    4,
                    new VikorScore
                    {
                        Q = 0.497,
                        R = 0.2,
                        S = 0.532,
                    }
                ),
                new(
                    8,
                    5,
                    new VikorScore
                    {
                        Q = 0.436,
                        R = 0.175,
                        S = 0.534,
                    }
                ),
                new(
                    9,
                    1,
                    new VikorScore
                    {
                        Q = 1.0,
                        R = 0.3,
                        S = 0.733,
                    }
                ),
                new(
                    10,
                    6,
                    new VikorScore
                    {
                        Q = 0.404,
                        R = 0.167,
                        S = 0.525,
                    }
                ),
            }
        );

        var data = DataBuilder
            .Create()
            .AddWeights(weights)
            .AddDecisionCriteria(types)
            .AddDecisionMatrix(matrix)
            .Build();

        var vikor = VikorBuilder
            .Create()
            .WithNormalizationMethod(NormalizationMethod.Vector)
            .WithParameters(parameters =>
            {
                parameters.WithV(0.5);
            })
            .Build();

        var result = vikor.Run(data);
        result.IsSuccess(out var value);

        var actual = value!
            .Select(x => new RankingRow<VikorScore>(
                x.Alternative,
                x.Rank,
                new VikorScore
                {
                    Q = x.Score.Q.Round(3),
                    R = x.Score.R.Round(3),
                    S = x.Score.S.Round(3),
                }
            ))
            .ToList();

        Assert.Equivalent(expectedVikorScore, new Ranking<VikorScore>(actual));
    }

    [Fact]
    public void Calculate_Promethee2Method_ShouldBeEqualToExpected()
    {
        var expectedRanking = new Ranking<double>(
            new List<RankingRow<double>>
            {
                new(1, 1, 0.448),
                new(2, 3, -0.292),
                new(3, 4, -0.41),
                new(4, 2, 0.254),
            }
        );

        double[,] matrix =
        {
            { 1900, 0.83, 24, 3.5, 8.5, 3, 4, 30, 4, 1 },
            { 2200, 0.77, 22, 3, 7.5, 1, 5, 14, 3, 1 },
            { 1300, 0.67, 20, 3, 5, 0, 4, 7, 3, 1 },
            { 1800, 0.74, 20, 4, 10, 0, 5, 14, 4, 3 },
        };

        double[] weights =
        {
            0.2122,
            0.1678,
            0.0633,
            0.0345,
            0.1123,
            0.0798,
            0.0112,
            0.0860,
            0.1584,
            0.0745,
        };

        int[] types = { -1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        var data = DataBuilder
            .Create()
            .AddWeights(weights)
            .AddDecisionCriteria(types)
            .AddDecisionMatrix(matrix)
            .Build();

        var promethee2 = Promethee2Builder
            .Create()
            .WithNormalizationMethod(NormalizationMethod.MinMax)
            .WithPreferenceFunction<FShapePreferenceFunctionBuilder>(builder =>
            {
                builder
                    .WithPreferenceThreshold(0.0)
                    .Build();
            })
            .Build();

        var result = promethee2.Run(data);
        result.IsSuccess(out var value);

        var actual = value!
            .RankingItems.Select(x => new RankingRow<double>(
                x.Alternative,
                x.Rank,
                x.Score.Round(3)
            ))
            .ToList();

        Assert.Equivalent(expectedRanking.RankingItems, actual);
    }
}
