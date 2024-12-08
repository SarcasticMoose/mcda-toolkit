using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Methods.Vikor;

public class VikorParameters : IMcdaAdditionalParameters
{
    public double V { get; }

    public static VikorParameters CreateDefault() =>  new(0.5);

    public static VikorParameters Create(double v)
    {
        if (v >= 1.0 && v < 0.0)
        {   
            throw new ArgumentOutOfRangeException(nameof(v), v, "Value must be between 0.0 and 1.0.");
        }
        return new VikorParameters(v);
    }

    private VikorParameters(double v)
    {
        V = v;
    }
}