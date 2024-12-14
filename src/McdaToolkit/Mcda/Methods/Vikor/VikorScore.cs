using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Methods.Vikor;

public record VikorScore(
    Vector<double> R,
    Vector<double> S,
    Vector<double> Q) : IMcdaScore
{
    /// <summary>
    /// Regret
    /// </summary>
    public Vector<double> R {get;} = R;

    /// <summary>
    /// Closeness to ideal
    /// </summary>
    public Vector<double> S {get;} = S;
    
    /// <summary>
    /// Compromise solution 
    /// </summary>
    public Vector<double> Q {get;} = Q;
}