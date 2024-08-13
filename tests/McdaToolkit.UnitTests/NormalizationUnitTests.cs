using FluentAssertions;
using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Enums;
using McdaToolkit.Normalization;
using McdaToolkit.Normalization.Service;
using McdaToolkit.UnitTests.Helpers;

namespace McdaToolkit.UnitTests;

public class NormalizationUnitTests
{
    private readonly Matrix<double> _matrixToNormalize = Matrix<double>.Build.DenseOfArray(new double[,]
    {
        { 32.57, 14.56, 87.12, 56.34, 47.89 },
        { 93.23, 76.34, 33.78, 25.68, 64.23 },
        { 78.45, 68.92, 45.67, 87.34, 39.45 },
        { 54.12, 34.56, 56.89, 92.56, 81.56 },
        { 21.76, 53.23, 73.21, 65.78, 57.34 },
        { 85.34, 98.45, 38.45, 23.45, 62.89 },
        { 42.68, 27.34, 49.67, 74.12, 53.21 }
    });
        
    private readonly int[] _types = [-1, -1, 1, 1, -1];
    
    [Fact]
    public void Normalize_MinMaxNormalization_ShouldReturnedExpectedValues()
    {
        var expected = new double[][]
        {
            [0.84874773, 1, 1, 0.47590797, 0.79957255],
            [0, 0.26355942, 0, 0.03226740, 0.41154120],
            [0.20680006, 0.35200858, 0.22290964, 0.92446824, 1],
            [0.54722261, 0.76159256, 0.43325834, 1, 0],
            [1, 0.53903922, 0.73922010, 0.61250181, 0.57516029],
            [0.11039597, 0, 0.08755156, 0, 0.44336262],
            [0.70728977, 0.84765765, 0.29790026, 0.73317899, 0.67323676]
        };
        var dataNormalization = new MatrixNormalizatorService(NormalizationMethod.MinMax);

        var normalizedMatrix = dataNormalization.NormalizeMatrix(_matrixToNormalize,_types);
        
        var equalityResult = TestHelpers.CheckEquality(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }
    
    [Fact]
    public void Normalize_VectorNormalization_ShouldReturnedExpectedValues()
    {
        var normalizationType = NormalizationMethod.Vector;
        var expected = new double[][]
        {
            [0.80678026, 0.90838501, 0.57002794, 0.32313221, 0.69529318],
            [0.44691814, 0.51965053, 0.22102323, 0.14728497, 0.59132764],
            [0.53459968, 0.56633894, 0.29881974, 0.50092948, 0.74899386],
            [0.67893607, 0.78254024, 0.37223243, 0.53086825, 0.48106309],
            [0.87090999, 0.66506416, 0.47901452, 0.37727434, 0.63516623],
            [0.49372513, 0.38052914, 0.25157913, 0.13449503, 0.59985358],
            [0.74680324, 0.82797021, 0.32499182, 0.42510755, 0.66144393]
        };
        var dataNormalization = new MatrixNormalizatorService(normalizationType);
        
        var normalizedMatrix = dataNormalization.NormalizeMatrix(_matrixToNormalize,_types);
        
        var equalityResult = TestHelpers.CheckEquality(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }
    
    [Fact]
    public void Normalize_Logarithmic_ShouldReturnedExpectedValues()
    {
        var normalizationType = NormalizationMethod.Logarithmic;
        var expected = new double[][]
        {
            [0.87403010, 0.89954564, 0.16128664, 0.14438272, 0.86315596],
            [0.83599827, 0.83739945, 0.12708112, 0.11624356, 0.85277256],
            [0.84224030, 0.84123458, 0.13796910, 0.16008394, 0.87001329],
            [0.85566609, 0.86712382, 0.14590034, 0.16216292, 0.84432373],
            [0.88861531, 0.85092357, 0.15500620, 0.14993079, 0.85678609],
            [0.83919604, 0.82785947, 0.13175623, 0.11299010, 0.85351828],
            [0.86425385, 0.87591345, 0.14100037, 0.15420595, 0.85943008]
        };
        var dataNormalization = new MatrixNormalizatorService(normalizationType);
        
        var normalizedMatrix = dataNormalization.NormalizeMatrix(_matrixToNormalize,_types);
        
        var equalityResult = TestHelpers.CheckEquality(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }
    
    [Fact]
    public void Normalize_Sum_ShouldReturnedExpectedValues()
    {
        var normalizationType = NormalizationMethod.Sum;
        var expected = new double[][]
        {
            [0.19968511, 0.36006754, 0.22640921, 0.13248054, 0.16546924],
            [0.06976021, 0.06867413, 0.08778814, 0.06038517, 0.12337416],
            [0.08290305, 0.07606766, 0.11868812, 0.20537541, 0.20087001],
            [0.12017266, 0.15169512, 0.14784688, 0.21764996, 0.09715942],
            [0.29888530, 0.09848926, 0.19025962, 0.15467820, 0.13819885],
            [0.07620980, 0.05325123, 0.09992463, 0.05514144, 0.12600289],
            [0.15238388, 0.19175506, 0.12908340, 0.17428928, 0.14892543]
        };
        var dataNormalization = new MatrixNormalizatorService(normalizationType);
        
        var normalizedMatrix = dataNormalization.NormalizeMatrix(_matrixToNormalize,_types);
        
        var equalityResult = TestHelpers.CheckEquality(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }
    
    [Fact]
    public void Normalize_Max_ShouldReturnedExpectedValues()
    {
        var normalizationType = NormalizationMethod.Max;
        var expected = new double[][]
        {
            [0.65064893, 0.85210767, 1, 0.60868626, 0.41282491],
            [0, 0.22458101, 0.38774105, 0.27744166, 0.21248161],
            [0.15853266, 0.29994921, 0.52421947, 0.94360415, 0.51630701],
            [0.41950016, 0.64895886, 0.65300735, 1, 0],
            [0.76659873, 0.45931945, 0.84033517, 0.71067416, 0.29695929],
            [0.08462941, 0, 0.44134527, 0.25334918, 0.22891123],
            [0.54220744, 0.72229558, 0.57013315, 0.80077787, 0.34759686]
        };
        var dataNormalization = new MatrixNormalizatorService(normalizationType);
        
        var normalizedMatrix = dataNormalization.NormalizeMatrix(_matrixToNormalize,_types);
        
        var equalityResult = TestHelpers.CheckEquality(normalizedMatrix, expected);
        equalityResult.Should().BeTrue();
    }
}