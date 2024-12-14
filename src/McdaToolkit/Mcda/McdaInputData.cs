using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Mcda.Methods.Abstraction;

namespace McdaToolkit.Mcda;

public record McdaInputData(
    Matrix<double> Matrix,
    Vector<double> Weights,
    int[] Types)
{
    public int[] Types { get; } = Types;
    public Matrix<double> Matrix { get; } = Matrix;
    public Vector<double> Weights { get; } = Weights;
}