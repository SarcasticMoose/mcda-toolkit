using McdaToolkit.Enums;

namespace McdaToolkit.Options;

public class McdaMethodOptions
{
    public NormalizationMethodEnum NormalizationMethodEnum { get; set; } = NormalizationMethodEnum.MinMax;
}