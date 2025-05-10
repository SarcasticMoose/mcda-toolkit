using MathNet.Numerics.LinearAlgebra;

<<<<<<<< HEAD:src/McdaToolkit/Methods/Promethee/II/PreferenceFunctions/Abstraction/IPreferenceFunction.cs
namespace McdaToolkit.Methods.Promethee.II.PreferenceFunctions.Abstraction;
========
namespace McdaToolkit.Methods.Promethee2.PreferenceFunctions.Abstraction;
>>>>>>>> cc9253a (feat: updated namespaces):src/McdaToolkit/Methods/Promethee2/PreferenceFunctions/Abstraction/IPreferenceFunction.cs

public interface IPreferenceFunction
{
    Matrix<double> Execute(Matrix<double> input);
}