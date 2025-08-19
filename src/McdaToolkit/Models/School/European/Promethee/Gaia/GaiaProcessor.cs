using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Data.Operations;

namespace McdaToolkit.Models.School.European.Promethee.Gaia;

internal class GaiaProcessor 
{
    private readonly PrincipalComponentAnalysis _pca;
    private readonly MatrixOperations _matrixOperations;
    
    public GaiaProcessor(
        PrincipalComponentAnalysis pca)
    {
        _pca = pca;
    }
    
    public GaiaPlane Execute(Matrix<double> normalizedMatrix, Vector<double> weights)
    {
        var weightedNormalizedMatrix = _matrixOperations.Weight(normalizedMatrix, weights);
        return new()
        {
            Projection = _pca.Transform(weightedNormalizedMatrix, 2),
            Vectors = _pca.Transform(weights.ToRowMatrix(), 2)
        };
    }
}