using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

/// <summary>
/// Defines the contract for preference functions used in PROMETHEE methods.
/// </summary>
public interface IPreferenceFunction
{
    /// <summary>
    /// Executes the preference function on an entire decision matrix.
    /// </summary>
    /// <param name="matrix">The input matrix containing pairwise differences between alternatives.</param>
    /// <returns>
    /// A matrix of the same dimensions as <paramref name="matrix"/>, 
    /// where each element is the evaluated preference value.
    /// </returns>
    Matrix<double> Execute(Matrix<double> matrix);
}