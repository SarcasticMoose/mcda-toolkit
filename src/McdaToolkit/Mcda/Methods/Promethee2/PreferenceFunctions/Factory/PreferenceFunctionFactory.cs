using McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Mcda.Methods.Promethee2.PreferenceFunctions.Factory;

public class PreferenceFunctionFactory
{
    public IPreferenceFunction Create(PreferenceFunction preferenceFunction)
    {
        return preferenceFunction switch
        {
            PreferenceFunction.Usual => new Usual(),
            PreferenceFunction.Ushape => new Ushape(),
            PreferenceFunction.Fshape => new Fshape(),
            _ => throw new ArgumentOutOfRangeException(nameof(preferenceFunction), preferenceFunction, null)
        };
    }
}