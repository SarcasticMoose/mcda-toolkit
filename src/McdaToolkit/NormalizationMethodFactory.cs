using LightResults;
using McdaToolkit.Normalization.Enums;
using McdaToolkit.Normalization.Methods;
using McdaToolkit.Normalization.Methods.Abstraction;
using McdaToolkit.Normalization.Methods.Geometric;
using McdaToolkit.Normalization.Methods.Linear;
using McdaToolkit.Normalization.Services.MatrixNormalizator.Errors;

namespace McdaToolkit;

internal class NormalizationMethodFactory
{
    public IVectorNormalizator<double> Create(NormalizationMethod method)
    {
        return method switch
        {
            NormalizationMethod.MinMax => new MinMaxNormalization(),
            NormalizationMethod.Vector => new VectorL2Normalization(),
            NormalizationMethod.Logarithmic => new LogarithmicNormalization(),
            NormalizationMethod.Sum => new SumNormalization(),
            NormalizationMethod.Max => new MaxNormalization(),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }
}