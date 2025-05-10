using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Methods.Promethee2.PreferenceFunctions.Abstraction;

public interface IPreferenceFunction
{
    Matrix<double> Execute(Matrix<double> input);
}