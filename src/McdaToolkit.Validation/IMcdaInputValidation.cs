using System.Numerics;
using LightResults;

namespace McdaToolkit.Validation;

public interface IMcdaInputValidation
{
    IResult Validate<T>(T[,]? matrix, List<CriterionDefinition<T>>? criteria)
        where T : struct, IFloatingPointIeee754<T>;
}
