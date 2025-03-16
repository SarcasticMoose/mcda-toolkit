using MathNet.Numerics.LinearAlgebra.Complex;
using McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions;

internal class Fshape : PreferenceFunctionBase
{
    Dictionary<Func<double,double>, Func<(double,double),bool>> criterias = new();
    private double _p;

    public Fshape()
    {
        criterias.Add(_ => 0.0, (parameters) => parameters.Item1 <= parameters.Item2);
        criterias.Add((dp) => dp, (parameters) => (0.0 <= parameters.Item1) && (parameters.Item1 <= parameters.Item2));
        criterias.Add(_ => 1.0, (parameters) => parameters.Item1 > parameters.Item2);
    }
    
    public override double ExecuteOne(double d)
    {
        double output = 0.0;
        foreach (var criteria in criterias)
        {
            if (criteria.Value.Invoke(new ValueTuple<double, double>(d,_p)))
            {
                output = criteria.Key.Invoke(d/_p);
                break;
            }
        }
        return output;
    }
}