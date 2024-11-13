using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Methods.Vikor;

public class VikorParameters : IMcdaAdditionalParameters
{
    public double V { get; }

    public static VikorParameters CreateDefault()
    {
        return new VikorParameters(0.5);
    }

    public static VikorParameters Create(int v)
    {
        return new VikorParameters(v);
    }

    private VikorParameters(double v)
    {
        V = v;
    }
}