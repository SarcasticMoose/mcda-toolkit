using LightResults;
using McdaToolkit.Data;
using McdaToolkit.Data.Operations.Normalization.Service.MatrixNormalizator;
using McdaToolkit.Data.Operations.Weighting;
using McdaToolkit.Models.Abstraction;
using McdaToolkit.Models.Rankings;
using McdaToolkit.Models.School.European.Promethee.Gaia;
using McdaToolkit.Models.School.European.Promethee.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.II.Gaia;

public class Promethee2Gaia : IMcdaMethod<Promethee2GaiaOutput>
{
    private readonly IPreferenceFunction _preferenceFunction;
    private readonly GaiaProcessor _gaiaProcessor;
    private readonly Promethee2Base _promethee2Base;
    private readonly IMatrixNormalizator _matrixNormalizator;
    private readonly IMatrixWeighter _matrixWeighter;

    internal Promethee2Gaia(
        IMatrixNormalizator matrixNormalizator,
        IMatrixWeighter matrixWeighter,
        GaiaProcessor gaiaProcessor,
        IPreferenceFunction preferenceFunction,
        Promethee2Base promethee2Base)
    {
        _matrixNormalizator = matrixNormalizator;
        _matrixWeighter = matrixWeighter;
        _gaiaProcessor = gaiaProcessor;
        _preferenceFunction = preferenceFunction;
        _promethee2Base = promethee2Base;
    }
    
    public IResult<Promethee2GaiaOutput> Run(McdaInputData data)
    {
        var normalizedMatrix = _matrixNormalizator.Normalize(data.Matrix, data.Types);
        var gaiaPlane = _gaiaProcessor.Execute(normalizedMatrix, data.Weights);
        var diffrentialMatrix = _promethee2Base.GetAlternativeDiffrence(normalizedMatrix);
        var afterPreferenceFunction = _preferenceFunction.Execute(diffrentialMatrix);
        var weightedPreferenceMatrix = _matrixWeighter.Weight(afterPreferenceFunction, data.Weights);
        var flowMatrix = _promethee2Base.AggregatePreferenceFlows(weightedPreferenceMatrix, normalizedMatrix.RowCount);
        var leavingFlows = flowMatrix.RowSums() / (flowMatrix.RowCount - 1);
        var enteringFlows = flowMatrix.ColumnSums() / (flowMatrix.ColumnCount - 1);
        var netFlows = leavingFlows - enteringFlows;
        return Result.Success(
            new Promethee2GaiaOutput(
                netFlows.CreateRanking(),
                gaiaPlane));
    }
}