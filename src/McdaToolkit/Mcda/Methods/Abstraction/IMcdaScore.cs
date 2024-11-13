using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Mcda.Methods.Abstraction;

public interface IMcdaScore
{
    Vector<double> Score { get; }
}