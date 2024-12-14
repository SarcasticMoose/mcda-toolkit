using FluentAssertions;
using MathNet.Numerics;
using McdaToolkit.Mcda.Factories;
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
}