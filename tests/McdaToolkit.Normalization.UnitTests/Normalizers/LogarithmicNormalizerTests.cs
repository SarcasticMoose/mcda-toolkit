using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.NonLinear;

namespace McdaToolkit.Normalization.UnitTests.Normalizers;

public class LogarithmicNormalizerTests
{
    private readonly LogarithmicNormalizer<double> _sut = new();

    [Fact]
    public void Normalize_ComputesLogRelativeToProductLog()
    {
        var data = Vector<double>.Build.DenseOfArray([Math.E, Math.E * Math.E, Math.E * Math.E * Math.E]);

        var result = _sut.Normalize(data);

        Assert.Equal(1.0 / 6.0, result[0], tolerance: 1e-10);
        Assert.Equal(2.0 / 6.0, result[1], tolerance: 1e-10);
        Assert.Equal(3.0 / 6.0, result[2], tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_AllOnesVector_ReturnsAllZeros()
    {
        // product = 1, ln(1) = 0 → all zeros
        var data = Vector<double>.Build.DenseOfArray([1.0, 1.0, 1.0]);

        var result = _sut.Normalize(data);

        Assert.All(result, v => Assert.Equal(0.0, v, tolerance: 1e-10));
    }

    [Fact]
    public void Normalize_ValuesNormalizeTo01Range()
    {
        var data = Vector<double>.Build.DenseOfArray([Math.E, Math.E * Math.E, Math.E * Math.E * Math.E]);

        var result = _sut.Normalize(data);

        Assert.All(result, v => Assert.True(v is >= 0.0 and <= 1.0));
    }
}
