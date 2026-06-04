using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Normalizers;

namespace McdaToolkit.Normalization.UnitTests.Normalizers;

public class VectorL2NormalizerTests
{
    private readonly VectorL2Normalizer<double> _sut = new();

    [Fact]
    public void Normalize_DividesByL2Norm()
    {
        // 3-4-5 right triangle: sqrt(9+16) = 5
        var data = Vector<double>.Build.DenseOfArray([3.0, 4.0]);

        var result = _sut.Normalize(data);

        Assert.Equal(0.6, result[0], tolerance: 1e-10);
        Assert.Equal(0.8, result[1], tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_ResultVectorHasUnitLength()
    {
        var data = Vector<double>.Build.DenseOfArray([1.0, 2.0, 3.0, 4.0]);

        var result = _sut.Normalize(data);

        var norm = Math.Sqrt(result.Select(v => v * v).Sum());
        Assert.Equal(1.0, norm, tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_ZeroVector_ReturnsAllZeros()
    {
        var data = Vector<double>.Build.DenseOfArray([0.0, 0.0, 0.0]);

        var result = _sut.Normalize(data);

        Assert.All(result, v => Assert.Equal(0.0, v, tolerance: 1e-10));
    }
}
