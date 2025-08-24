using McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Functions.UShape;

/// <summary>
/// Represents the <b>U-Shape Preference Function</b> used in PROMETHEE.
/// </summary>
/// <remarks>
/// This function introduces an <b>indifference threshold q</b>. 
/// If the performance difference is small (≤ q), the alternatives are treated as indifferent. 
/// If the difference is larger, full preference is applied.
/// </remarks>
public class UShapePreferenceFunction : PreferenceFunctionBase
{
    private Dictionary<Func<double,double>, Func<(double,double),bool>> _criterias = new();
    private double _q;
    
    public UShapePreferenceFunction(double q)
    {
        _q = q;
        _criterias.Add(_ => 0.0,parameters => parameters.Item1 <= parameters.Item2);
        _criterias.Add(_ => 1.0,parameters => parameters.Item1 > parameters.Item2);
    }

    /// <summary>
    /// Computes the preference value for a single difference <paramref name="d"/>.
    /// </summary>
    /// <param name="d">
    /// The difference between performances of two alternatives for a criterion.
    /// </param>
    /// <returns>
    /// <list type="bullet">
    /// <item><description>0 if <paramref name="d"/> ≤ <c>_q</c> (indifference).</description></item>
    /// <item><description>1 if <paramref name="d"/> &gt; <c>_q</c> (strict preference).</description></item>
    /// </list>
    /// </returns>
    protected override double ExecuteOne(double d)
    {
        double output = 0.0;
        foreach (var criteria in _criterias)
        {
            if (criteria.Value.Invoke(new ValueTuple<double,double>(d,_q)))
            {
                output = criteria.Key.Invoke(d);
                break;
            }
        }
        return output;   
    }
}