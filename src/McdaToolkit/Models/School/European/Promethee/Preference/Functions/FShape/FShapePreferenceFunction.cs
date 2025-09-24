using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Functions.FShape;

/// <summary>
/// Represents the <b>(F-Shape) Preference Function</b> used in PROMETHEE.
/// </summary>
/// <remarks>
/// This function introduces a <b>preference threshold p</b>. 
/// Preference increases linearly with the difference until it reaches 1 at threshold p.
/// </remarks>
public class FShapePreferenceFunction : PreferenceFunctionBase
{
    Dictionary<Func<double,double>, Func<(double,double),bool>> _criterias = new();
    private double _p;

    public FShapePreferenceFunction(double threshold)
    {
        _p = threshold;
        _criterias.Add(_ => 0.0, (parameters) => parameters.Item1 <= parameters.Item2);
        _criterias.Add((dp) => dp, (parameters) => (0.0 <= parameters.Item1) && (parameters.Item1 <= parameters.Item2));
        _criterias.Add(_ => 1.0, (parameters) => parameters.Item1 > parameters.Item2);
    }

    /// <summary>
    /// Computes the preference value for a single difference <paramref name="d"/>.
    /// </summary>
    /// <param name="d">
    /// The difference between performances of two alternatives for a criterion.
    /// </param>
    /// <returns>
    /// <list type="bullet">
    /// <item><description>0 if <paramref name="d"/> ≤ 0 (no preference).</description></item>
    /// <item><description>(<paramref name="d"/> / <c>_p</c>) if 0 &lt; <paramref name="d"/> &lt; <c>_p</c> (linear growth of preference).</description></item>
    /// <item><description>1 if <paramref name="d"/> ≥ <c>_p</c> (full preference).</description></item>
    /// </list>
    /// </returns>
    protected override double ExecuteOne(double d)
    {
        double output = 0.0;
        foreach (var criteria in _criterias)
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