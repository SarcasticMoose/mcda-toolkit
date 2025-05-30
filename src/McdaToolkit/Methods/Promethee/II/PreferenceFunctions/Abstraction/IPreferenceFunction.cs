using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Methods.Promethee.II.PreferenceFunctions.Abstraction;

public interface IPreferenceFunction
{
    Matrix<double> Execute(Matrix<double> input);
}