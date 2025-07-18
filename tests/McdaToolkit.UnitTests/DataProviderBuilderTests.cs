using FluentAssertions;
using McdaToolkit.Data.Builders;
using McdaToolkit.Data.Validation.Abstraction;

namespace McdaToolkit.UnitTests;

public class DataProviderBuilderTests
{
    [Fact]
    public void Build_WithValidInputs_ShouldReturnCorrectMcdaData()
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

        var data = new DataBuilder()
            .AddWeights(weights)
            .AddDecisionCriteria(types)
            .AddDecisionMatrix(matrix)
            .Build();
        
        data.Should().NotBeNull();
        data.Weights.Storage.ToArray().Should().BeEquivalentTo(weights);
        data.Types.Should().BeEquivalentTo(types);
        data.Matrix.Storage.ToArray().Should().BeEquivalentTo(matrix);
    }
    
    [Fact]
    public void Build_WithoutDecisionMatrix_ShouldThrowArgumentNullException()
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
        
        Action act = () =>
        {
            new DataBuilder()
                .AddWeights(weights)
                .AddDecisionCriteria(types)
                .Build();
        };

        act.Should().Throw<ValidationException>();
    }
}