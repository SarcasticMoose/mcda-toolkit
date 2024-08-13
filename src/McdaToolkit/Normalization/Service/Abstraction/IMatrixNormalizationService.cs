using LightResults;
using McdaToolkit.Enums;

namespace McdaToolkit.Normalization.Service.Abstraction;

internal interface IMatrixNormalizationService : IMatrixNormalizator<double>
{
    public NormalizationMethod GetCurrentNormalizationName { get; }
    public Result ChangeNormalizationMethod(NormalizationMethod newMethod);
}