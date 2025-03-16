using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions;

internal class Usual : PreferenceFunctionBase
{
    Dictionary<Func<double>, Func<double,bool>> criterias = new();

    public Usual()
    {
        criterias.Add(() => 0.0, (parameters) => parameters <= 0);
        criterias.Add(() => 0.0, (parameters) => parameters >= 1);
    }
   
    public override double ExecuteOne(double d)
    {
        double output = 0.0;
        foreach (var criteria in criterias)
        {
            if (criteria.Value.Invoke(d))
            {
                output = criteria.Key.Invoke();
                break;
            }
        }
        return output;
    }
}