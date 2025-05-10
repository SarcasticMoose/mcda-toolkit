using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Shared.Data;

public record McdaInputData(
    Matrix<double> Matrix,
    Vector<double> Weights,
    int[] Types)
{
    public int[] Types { get; } = Types;
    public Matrix<double> Matrix { get; } = Matrix;
    public Vector<double> Weights { get; } = Weights;
}