<<<<<<<< HEAD:src/McdaToolkit/Methods/Promethee/II/PreferenceFunctions/Ushape.cs
using McdaToolkit.Methods.Promethee.II.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Methods.Promethee.II.PreferenceFunctions;
========
using McdaToolkit.Methods.Promethee2.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Methods.Promethee2.PreferenceFunctions;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Methods/Promethee2/PreferenceFunctions/Ushape.cs

internal class Ushape : PreferenceFunctionBase
{
    Dictionary<Func<double,double>, Func<(double,double),bool>> criterias = new();
    private double Q;
    
    public Ushape()
    {
        criterias.Add(_ => 0.0,parameters => parameters.Item1 <= parameters.Item2);
        criterias.Add(_ => 1.0,parameters => parameters.Item1 > parameters.Item2);
    }

    public override double ExecuteOne(double d)
    {
        double output = 0.0;
        foreach (var criteria in criterias)
        {
            if (criteria.Value.Invoke(new ValueTuple<double,double>(d,Q)))
            {
                output = criteria.Key.Invoke(d);
                break;
            }
        }
        return output;   
    }
}