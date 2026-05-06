using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Functions.Usual;

/// <summary>
/// Represents the <b>Usual Preference Function</b> used in PROMETHEE.
/// </summary>
/// <remarks>
/// This function expresses strict preference only when a positive difference exists.
/// It is the simplest preference function, with no thresholds.
/// </remarks>
public class UsualPreferenceFunction : PreferenceFunctionBase
{
    Dictionary<Func<double>, Func<double,bool>> criterias = new();

    public UsualPreferenceFunction()
    {
        criterias.Add(() => 0.0, (parameters) => parameters <= 0);
        criterias.Add(() => 1.0, (parameters) => parameters > 0);
    }

    /// <summary>
    /// Computes the preference value for a single difference <paramref name="d"/>.
    /// </summary>
    /// <param name="d">
    /// The difference between performances of two alternatives for a criterion 
    /// (alternative A - alternative B).
    /// </param>
    /// <returns>
    /// <list type="bullet">
    /// <item><description>0 if <paramref name="d"/> ≤ 0 (no preference).</description></item>
    /// <item><description>1 if <paramref name="d"/> &gt; 0 (strict preference).</description></item>
    /// </list>
    /// </returns>
    protected override double ExecuteOne(double d)
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