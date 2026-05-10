using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Transformers;

namespace McdaToolkit.Normalization.UnitTests.Transformers;

public class CostTransformerTests
{
    private readonly CostTransformer<double> _sut = new();

    [Fact]
    public void Transform_ComputesMaxMinusValue()
    {
        var data = Vector<double>.Build.DenseOfArray([1.0, 3.0, 5.0]);

        var result = _sut.Transform(data);

        Assert.Equal(4.0, result[0], tolerance: 1e-10);
        Assert.Equal(2.0, result[1], tolerance: 1e-10);
        Assert.Equal(0.0, result[2], tolerance: 1e-10);
    }

    [Fact]
    public void Transform_MaxValueBecomesZero()
    {
        var data = Vector<double>.Build.DenseOfArray([2.0, 5.0, 8.0]);

        var result = _sut.Transform(data);

        Assert.Equal(0.0, result.Min(), tolerance: 1e-10);
    }

    [Fact]
    public void Transform_MinValueBecomesRange()
    {
        var data = Vector<double>.Build.DenseOfArray([2.0, 5.0, 8.0]);

        var result = _sut.Transform(data);

        Assert.Equal(6.0, result.Max(), tolerance: 1e-10);
    }

    [Fact]
    public void Transform_AllSameValues_ReturnsAllZeros()
    {
        var data = Vector<double>.Build.DenseOfArray([5.0, 5.0, 5.0]);

        var result = _sut.Transform(data);

        Assert.All(result, v => Assert.Equal(0.0, v, tolerance: 1e-10));
    }
}
