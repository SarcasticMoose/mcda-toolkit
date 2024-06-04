using FluentAssertions;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Enums;
using McdaToolkit.Normalization;

namespace McdaToolkit.UnitTests;

public class NormalizationUnitTests
{
    [Fact]
    public void Normalize_MinMaxNormalization_ShouldReturnedExpectedValues()
    {
        var matrixToNormalize = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            { 66, 56, 95 },
            { 61, 55, 166 },
            { 65, 49, 113 },
            { 95, 56, 99 },
            { 63, 43, 178 },
            { 74, 59, 140 },
        });
        
        var expected = new double[][]
        {
            [0.85294118, 0.1875, 0.0], 
            [1.0, 0.25, 0.85542169],
            [0.88235294, 0.625, 0.21686747],
            [0.0, 0.1875, 0.04819277],
            [0.94117647, 1.0, 1.0],
            [0.61764706, 0.0, 0.54216867]
        };
        
        int[] types = [-1,-1, 1];
        
        var dataNormalization = new DataNormalizationService(NormalizationMethodEnum.MinMax);

        var normalizedMatrix = dataNormalization.NormalizeMatrix(matrixToNormalize,types);

        var equalityResult = normalizedMatrix
            .EnumerateRows()
            .Select((x, i) => Helpers.TestHelpers.IsResultTheSame(x, expected[i]))
            .All(y => y);

        equalityResult.Should().BeTrue();
    }
}