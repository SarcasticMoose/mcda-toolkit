using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda.Methods.Topsis;

public record TopsisScore(Vector<double> V) : IMcdaScore
{
    /// <summary>
    /// Preference values for alternative
    /// </summary>
    public Vector<double> V { get; } = V;
}