using McdaToolkit.Models.Abstraction;
using McdaToolkit.Models.Rankings;

namespace McdaToolkit.Models.School.European.Promethee.II;

public class Promethee2Output : IModelOutput
{
    public Promethee2Output(Ranking<double> ranking)
    {
        Ranking = ranking;
    }
    
    public Ranking<double> Ranking { get; }
}