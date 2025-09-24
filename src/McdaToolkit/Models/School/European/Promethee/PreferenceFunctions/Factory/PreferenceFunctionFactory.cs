using McdaToolkit.Models.School.European.Promethee.PreferenceFunctions.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.PreferenceFunctions.Factory;

public class PreferenceFunctionFactory
{
    public IPreferenceFunction Create(PreferenceFunction preferenceFunction)
    {
        return preferenceFunction switch
        {
            PreferenceFunction.Usual => new Usual(),
            PreferenceFunction.Ushape => new Ushape(),
            PreferenceFunction.Fshape => new Fshape(),
            _ => throw new ArgumentOutOfRangeException(
                nameof(preferenceFunction),
                preferenceFunction,
                null
            ),
        };
    }
}
