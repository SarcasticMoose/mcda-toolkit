using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Methods.Topsis;

public record TopsisScore(Vector<double> Score) : IMcdaScore
{
    public Vector<double> Score { get; } = Score;
}