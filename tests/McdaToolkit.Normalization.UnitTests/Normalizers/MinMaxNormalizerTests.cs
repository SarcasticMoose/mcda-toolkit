using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Linear;

namespace McdaToolkit.Normalization.UnitTests.Normalizers;

public class MinMaxNormalizerTests
{
    private readonly MinMaxNormalizer<double> _sut = new();

    [Fact]
    public void Normalize_ScalesToZeroAndOne()
    {
        var data = Vector<double>.Build.DenseOfArray([1.0, 3.0, 5.0]);

        var result = _sut.Normalize(data);

        Assert.Equal(0.0, result[0], tolerance: 1e-10);
        Assert.Equal(0.5, result[1], tolerance: 1e-10);
        Assert.Equal(1.0, result[2], tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_MaxIsAlwaysOne()
    {
        var data = Vector<double>.Build.DenseOfArray([10.0, 40.0, 100.0]);

        var result = _sut.Normalize(data);

        Assert.Equal(1.0, result.Max(), tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_MinIsAlwaysZero()
    {
        var data = Vector<double>.Build.DenseOfArray([2.0, 5.0, 8.0]);

        var result = _sut.Normalize(data);

        Assert.Equal(0.0, result.Min(), tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_PreservesRelativeOrder()
    {
        var data = Vector<double>.Build.DenseOfArray([1.0, 2.0, 4.0, 8.0]);

        var result = _sut.Normalize(data);

        Assert.True(result[0] < result[1]);
        Assert.True(result[1] < result[2]);
        Assert.True(result[2] < result[3]);
    }
}
