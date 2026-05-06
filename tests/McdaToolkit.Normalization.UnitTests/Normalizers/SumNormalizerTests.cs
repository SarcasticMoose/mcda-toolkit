using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Linear;

namespace McdaToolkit.Normalization.UnitTests.Normalizers;

public class SumNormalizerTests
{
    private readonly SumNormalizer<double> _sut = new();

    [Fact]
    public void Normalize_DividesByColumnSum()
    {
        var data = Vector<double>.Build.DenseOfArray([1.0, 2.0, 7.0]);

        var result = _sut.Normalize(data);

        Assert.Equal(0.1, result[0], tolerance: 1e-10);
        Assert.Equal(0.2, result[1], tolerance: 1e-10);
        Assert.Equal(0.7, result[2], tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_NormalizedValuesSumToOne()
    {
        var data = Vector<double>.Build.DenseOfArray([3.0, 5.0, 2.0, 10.0]);

        var result = _sut.Normalize(data);

        Assert.Equal(1.0, result.Sum(), tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_ZeroSum_ReturnsAllZeros()
    {
        var data = Vector<double>.Build.DenseOfArray([0.0, 0.0, 0.0]);

        var result = _sut.Normalize(data);

        Assert.All(result, v => Assert.Equal(0.0, v, tolerance: 1e-10));
    }
}
