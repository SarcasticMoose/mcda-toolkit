using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Models.School.European.Promethee.PreferenceFunctions.Abstraction;

public interface IPreferenceFunction
{
    Matrix<double> Execute(Matrix<double> input);
}
