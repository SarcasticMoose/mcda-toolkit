using FluentAssertions;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data.Normalization.Methods.Geometric;
using McdaToolkit.Data.Normalization.Methods.Linear;
using McdaToolkit.Data.Normalization.Methods.NonLinear;

namespace McdaToolkit.UnitTests;

public class NormalizationTests
{
    [Fact]
    public void Normalize_MinMaxNormalization_ShouldReturnedExpectedValues()
    {
        var columnToNormalize = Vector<double>
            .Build
            .DenseOfArray([32.57, 93.23, 78.45, 54.12, 21.76, 85.34, 42.68]);
        var cost = true;
        double[] expected = [0.84874773, 0, 0.20680006, 0.54722261, 1, 0.11039597, 0.70728977];
        var dataNormalization = new MinMaxNormalization();
        var normalizedVector = dataNormalization.Normalize(columnToNormalize, cost);

        var equalityResult = TestHelpers.IsResultTheSame(normalizedVector, expected);
        equalityResult.Should().BeTrue();
    }

    [Fact]
    public void Normalize_VectorNormalization_ShouldReturnedExpectedValues()
    {
        var columnToNormalize = Vector<double>
            .Build
            .DenseOfArray([32.57, 93.23, 78.45, 54.12, 21.76, 85.34, 42.68]);
        var cost = true;
        double[] expected = [0.80678026, 0.44691814, 0.53459968, 0.67893607, 0.87090999, 0.49372513, 0.74680324];
        var dataNormalization = new VectorL2Normalization();
        var normalizedMatrix = dataNormalization.Normalize(columnToNormalize, cost);

        var equalityResult = TestHelpers.IsResultTheSame(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }

    [Fact]
    public void Normalize_Logarithmic_ShouldReturnedExpectedValues()
    {
        var columnToNormalize = Vector<double>
            .Build
            .DenseOfArray([32.57, 93.23, 78.45, 54.12, 21.76, 85.34, 42.68]);
        var cost = true;
        double[] expected = [0.87403010, 0.83599827, 0.84224030, 0.85566609, 0.88861531, 0.83919604, 0.86425385];
        var dataNormalization = new LogarithmicNormalization();
        var normalizedMatrix = dataNormalization.Normalize(columnToNormalize, cost);

        var equalityResult = TestHelpers.IsResultTheSame(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }

    [Fact]
    public void Normalize_Sum_ShouldReturnedExpectedValues()
    {
        var columnToNormalize = Vector<double>
            .Build
            .DenseOfArray([32.57, 93.23, 78.45, 54.12, 21.76, 85.34, 42.68]);
        var cost = true;
        double[] expected = [0.19968511, 0.06976021, 0.08290305, 0.12017266, 0.29888530, 0.07620980, 0.15238388];
        var dataNormalization = new SumNormalization();
        var normalizedMatrix = dataNormalization.Normalize(columnToNormalize, cost);

        var equalityResult = TestHelpers.IsResultTheSame(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }

    [Fact]
    public void Normalize_Max_ShouldReturnedExpectedValues()
    {
        var columnToNormalize = Vector<double>
            .Build
            .DenseOfArray([32.57, 93.23, 78.45, 54.12, 21.76, 85.34, 42.68]);
        var cost = true;
        double[] expected = [0.65064893, 0, 0.15853266, 0.41950016, 0.76659873, 0.08462941, 0.54220744];
        var dataNormalization = new MaxNormalization();
        var normalizedMatrix = dataNormalization.Normalize(columnToNormalize, cost);
        var equalityResult = TestHelpers.IsResultTheSame(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }
}