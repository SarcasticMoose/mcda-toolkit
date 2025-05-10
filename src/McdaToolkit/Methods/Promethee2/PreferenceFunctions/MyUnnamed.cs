<<<<<<<< HEAD:src/McdaToolkit/Methods/Promethee/II/PreferenceFunctions/MyUnnamed.cs
using McdaToolkit.Methods.Promethee.II.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Methods.Promethee.II.PreferenceFunctions;
========
using McdaToolkit.Methods.Promethee2.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Methods.Promethee2.PreferenceFunctions;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Methods/Promethee2/PreferenceFunctions/MyUnnamed.cs

internal class MyUnnamed : PreferenceFunctionBase
{
    Dictionary<Func<double,double>, Func<double,bool>> criterias = new();

    public MyUnnamed()
    {
        criterias.Add((_) => 0.0, (parameters) => parameters <= 0);
        criterias.Add((parameter) => parameter, (parameters) => parameters > 0);
    }
    
    public override double ExecuteOne(double d)
    {
        double output = 0.0;
        foreach (var criteria in criterias)
        {
            if (criteria.Value.Invoke(d))
            {
                output = criteria.Key.Invoke(d);
                break;
            }
        }
        return output;   
    }
}