<<<<<<<< HEAD:src/McdaToolkit/Methods/Promethee/II/PreferenceFunctions/Factory/PreferenceFunctionFactory.cs
using McdaToolkit.Methods.Promethee.II.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Methods.Promethee.II.PreferenceFunctions.Factory;
========
using McdaToolkit.Methods.Promethee2.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Methods.Promethee2.PreferenceFunctions.Factory;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Methods/Promethee2/PreferenceFunctions/Factory/PreferenceFunctionFactory.cs

public class PreferenceFunctionFactory
{
    public IPreferenceFunction Create(PreferenceFunction preferenceFunction)
    {
        return preferenceFunction switch
        {
            PreferenceFunction.Usual => new Usual(),
            PreferenceFunction.Ushape => new Ushape(),
            PreferenceFunction.Fshape => new Fshape(),
            PreferenceFunction.Unnamed => new MyUnnamed(),
            _ => throw new ArgumentOutOfRangeException(nameof(preferenceFunction), preferenceFunction, null)
        };
    }
}