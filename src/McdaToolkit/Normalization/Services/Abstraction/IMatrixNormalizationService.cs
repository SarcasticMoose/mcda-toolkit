using LightResults;
using McdaToolkit.Normalization.Enums;
using McdaToolkit.Normalization.Methods.Abstraction;

namespace McdaToolkit.Normalization.Services.Abstraction;

internal interface IMatrixNormalizationService : IMatrixNormalizator<double>
{
    public NormalizationMethod GetCurrentNormalizationName { get; }
    public IResult ChangeNormalizationMethod(NormalizationMethod newMethod);
}