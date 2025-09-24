using McdaToolkit.Data.Normalization;
using McdaToolkit.Data.Normalization.Services.MatrixNormalizator;
using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;
using McdaToolkit.Models.School.European.Promethee.Preference.Functions.Usual;

namespace McdaToolkit.Models.School.European.Promethee.II;

/// <summary>
/// Builder for configuring and creating an instance of the PROMETHEE II method.
/// </summary>
/// <remarks>
/// By default, if no preference function is specified by the user,
/// the <see cref="UsualPreferenceFunction"/> is used.
/// </remarks>
public sealed class Promethee2Builder
{
    private NormalizationMethod _normalizationMethod;
    private IPreferenceFunction? _preferenceFunction;

    /// <summary>
    /// Creates a new instance of <see cref="Promethee2Builder"/>.
    /// </summary>
    public static Promethee2Builder Create() => new();
    
    /// <summary>
    /// Sets the normalization method that will be applied
    /// during the PROMETHEE II computation.
    /// </summary>
    /// <param name="normalizationMethod">
    /// The normalization method used to transform the decision matrix.
    /// </param>
    /// <returns>
    /// The current <see cref="Promethee2Builder"/> instance to allow fluent configuration.
    /// </returns>
    public Promethee2Builder WithNormalizationMethod(NormalizationMethod normalizationMethod)
    {
        _normalizationMethod = normalizationMethod;
        return this;
    }

    /// <summary>
    /// Sets the preference function for the PROMETHEE II method.
    /// </summary>
    /// <typeparam name="TBuilder">
    /// The type of the preference function builder implementing
    /// <see cref="IPreferenceFunctionBuilder{T}"/>.
    /// </typeparam>
    /// <param name="action">
    /// Optional configuration action applied to the preference function builder.
    /// </param>
    /// <returns>
    /// The current <see cref="Promethee2Builder"/> instance to allow fluent configuration.
    /// </returns>
    public Promethee2Builder WithPreferenceFunction<TBuilder>(Action<TBuilder>? action = null)
        where TBuilder : IPreferenceFunctionBuilder<IPreferenceFunction>, new()
    {
        var instance = new TBuilder();
        if (action is null)
        {
            _preferenceFunction = instance.Build();
            return this;
        }
        action.Invoke(instance);
        _preferenceFunction = instance.Build();
        return this;
    }
    
    /// <summary>
    /// Builds a new instance of the <see cref="Promethee2"/> method
    /// using the configured parameters.
    /// </summary>
    /// <remarks>
    /// If no preference function has been explicitly provided,
    /// the <see cref="UsualPreferenceFunction"/> is used by default.
    /// </remarks>
    /// <returns>
    /// A fully configured <see cref="Promethee2"/> instance.
    /// </returns>
    public Promethee2 Build()
    {
        var normalizationMethod = new NormalizationMethodFactory().Create(_normalizationMethod);
        var normalizationService = new MatrixNormalizatorService(normalizationMethod);
        _preferenceFunction ??= new UsualPreferenceFunctionBuilder().Build();
        
        return new Promethee2(normalizationService, _preferenceFunction);
    }
}