using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Normalizers;

namespace McdaToolkit.Normalization.UnitTests.Normalizers;

public class MaxNormalizerTests
{
    private readonly MaxNormalizer<double> _sut = new();

    [Fact]
    public void Normalize_DividesByMax()
    {
        var data = Vector<double>.Build.DenseOfArray([2.0, 4.0, 8.0]);

        var result = _sut.Normalize(data);

        Assert.Equal(0.25, result[0], tolerance: 1e-10);
        Assert.Equal(0.50, result[1], tolerance: 1e-10);
        Assert.Equal(1.00, result[2], tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_MaxValueBecomesOne()
    {
        var data = Vector<double>.Build.DenseOfArray([3.0, 6.0, 12.0]);

        var result = _sut.Normalize(data);

        Assert.Equal(1.0, result.Max(), tolerance: 1e-10);
    }

    [Fact]
    public void Normalize_AllValuesInZeroToOneRange()
    {
        var data = Vector<double>.Build.DenseOfArray([1.0, 5.0, 10.0]);

        var result = _sut.Normalize(data);

        Assert.All(result, v => Assert.True(v is >= 0.0 and <= 1.0));
    }
}
