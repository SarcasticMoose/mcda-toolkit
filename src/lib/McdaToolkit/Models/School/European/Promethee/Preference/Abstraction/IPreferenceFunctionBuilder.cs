namespace McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

/// <summary>
/// Represents a generic builder for preference functions used in PROMETHEE methods.
/// </summary>
/// <typeparam name="TPreferenceFunction">
/// The type of preference function this builder creates. Must implement <see cref="IPreferenceFunction"/>.
/// </typeparam>
public interface IPreferenceFunctionBuilder<TPreferenceFunction>
    where TPreferenceFunction : IPreferenceFunction
{
    /// <summary>
    /// Builds and returns an instance of the preference function configured by this builder.
    /// </summary>
    /// <returns>An instance of <typeparamref name="TPreferenceFunction"/>.</returns>
    public TPreferenceFunction Build();
}