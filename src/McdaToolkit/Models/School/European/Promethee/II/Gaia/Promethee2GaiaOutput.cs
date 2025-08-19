using McdaToolkit.Models.Abstraction;
using McdaToolkit.Models.Rankings;
using McdaToolkit.Models.School.European.Promethee.Gaia;

namespace McdaToolkit.Models.School.European.Promethee.II.Gaia;

public record Promethee2GaiaOutput : IModelOutput
{
    public Promethee2GaiaOutput(
        Ranking<double> ranking, 
        GaiaPlane gaiaPlane)
    {
        Ranking = ranking;
        GaiaPlane = gaiaPlane;
    }

    public Ranking<double> Ranking { get; }
    public GaiaPlane GaiaPlane { get; }
}