using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Methods.Vikor;

public record VikorScore(Vector<double> R, Vector<double> S, Vector<double> Q) : IMcdaScore
{
    public Vector<double> R {get;} = R;
    public Vector<double> S {get;} = S;
    public Vector<double> Q {get;} = Q;

    public Vector<double> Score => Q;
}