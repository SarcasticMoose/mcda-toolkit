using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Functions.Usual;

public class UsualPreferenceFunctionBuilder : IPreferenceFunctionBuilder<IPreferenceFunction>
{
    public IPreferenceFunction Build() => new UsualPreferenceFunction();
}