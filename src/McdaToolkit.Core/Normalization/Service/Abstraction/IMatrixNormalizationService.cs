using LightResults;
using McdaToolkit.Core.Enums;

namespace McdaToolkit.Core.Normalization.Service.Abstraction;

internal interface IMatrixNormalizationService : IMatrixNormalizator<double>
{
    public NormalizationMethod GetCurrentNormalizationName { get; }
    public Result ChangeNormalizationMethod(NormalizationMethod newMethod);
}