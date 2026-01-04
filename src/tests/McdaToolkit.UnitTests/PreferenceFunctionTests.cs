using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Models.School.European.Promethee.Preference.Functions.FShape;
using McdaToolkit.Models.School.European.Promethee.Preference.Functions.UShape;
using McdaToolkit.Models.School.European.Promethee.Preference.Functions.Usual;

namespace McdaToolkit.UnitTests;

public class PreferenceFunctionTests
{
    Matrix<double>? _matrix = Matrix<double>.Build.DenseOfArray(new double[,]
    {
        { 0.33333333333333331, 1, 1, 0.5, 0.69999999999999996, 1, 0, 1, 1, 0 },
        { 0, 0.62500000000000022, 0.5, 0, 0.5, 0.33333333333333331, 1, 0.30434782608695654, 0, 0 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0.44444444444444442, 0.43749999999999989, 0, 1, 1, 0, 1, 0.30434782608695654, 1, 1 }
    });

    [Fact]
    public void Execute_WithMatrix_ShouldReturnExpectedUShape()
    {
        var expectedUShape = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            { 1, 1, 1, 1, 1, 1, 0, 1, 1, 0 },
            { 0, 1, 1, 0, 1, 1, 1, 1, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 0, 1, 1, 0, 1, 1, 1, 1 }
        });
        var preferenceFunction = new UShapePreferenceFunction(0.0);
        var result = preferenceFunction.Execute(_matrix!);

        Assert.Equivalent(expectedUShape, result);
    }

    [Fact]
    public void Execute_WithMatrix_ShouldReturnExpectedUsual()
    {
        var expectedUsual = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            { 1, 1, 1, 1, 1, 1, 0, 1, 1, 0 },
            { 0, 1, 1, 0, 1, 1, 1, 1, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 0, 1, 1, 0, 1, 1, 1, 1 }
        });
        var preferenceFunction = new UsualPreferenceFunction();
        var result = preferenceFunction.Execute(_matrix!);

        Assert.Equivalent(expectedUsual, result);
    }

    [Fact]
    public void Execute_WithMatrix_ShouldReturnExpectedFShape()
    {
        var expectedFShape = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            { 0.3333333333333333, 1, 1, 0.5, 0.7, 1, 0, 1, 1, 0 },
            { 0, 0.625, 0.5, 0, 0.5, 0.3333333333333333, 1, 0.30434782608695654, 0, 0 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0.4444444444444444, 0.4375, 0, 1, 1, 0, 1, 0.30434782608695654, 1, 1 }
        });

        var preferenceFunction = new FShapePreferenceFunction(1.0);
        var result = preferenceFunction.Execute(_matrix!);

        Assert.True(result.EnumerateIndexed()
            .All(e => Math.Abs(e.Item3 - expectedFShape[e.Item1, e.Item2]) < 1e-8));
    }
}