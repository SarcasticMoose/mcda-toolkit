using LightResults;
using McdaToolkit.Mcda.Methods.Abstraction;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Mcda.Methods.PrometheeII;

public class PrometheeII : McdaMethodBase<PrometheeIIScore>
{
    private readonly MatrixNormalizatorService _normalizationServiceServiceService;

    internal PrometheeII(MatrixNormalizatorService matrixNormalizationServiceService)
    {
        _normalizationServiceServiceService = matrixNormalizationServiceService;
    }
    
    public override IResult<PrometheeIIScore> Run(McdaInputData data)
    {
        throw new NotImplementedException();
    }
}