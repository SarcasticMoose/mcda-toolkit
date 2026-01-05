using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Models.School.European.Promethee.Preference.Abstraction;

/// <summary>
/// Provides a base implementation for preference functions used in PROMETHEE family methods
/// </summary>
public abstract class PreferenceFunctionBase : IPreferenceFunction
{
    /// <summary>
    /// <inheritdoc cref="IPreferenceFunction.Execute"/>
    /// Each element of the matrix is transformed by the <see cref="ExecuteOne"/> method.
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="IPreferenceFunction.Execute"/>
    /// </returns>
    public Matrix<double> Execute(Matrix<double> matrix)
    {
        var output = Matrix<double>.Build.Dense(matrix.RowCount, matrix.ColumnCount);
        foreach (var (i, row) in matrix.EnumerateRowsIndexed())
        {
            output.SetRow(i, ExecuteRow(row));
        }
        return output;
    }

    /// <summary>
    /// Executes the preference function on a single row vector.
    /// This method applies <see cref="ExecuteOne"/> to each element of the row.
    /// </summary>
    /// <param name="input">The input row vector of differences.</param>
    /// <returns>
    /// A vector of the same size as <paramref name="input"/>, 
    /// with each value transformed using the preference function.
    /// </returns>
    private Vector<double> ExecuteRow(Vector<double> input)
    {
        var output = Vector<double>.Build.Dense(input.Count);
        foreach (var (index, d) in input.EnumerateIndexed())
        {
            output[index] = ExecuteOne(d);
        }
        return output;
    }

    /// <summary>
    /// Defines the transformation logic for a single difference value.
    /// Derived classes must implement this method to provide 
    /// the specific preference function (e.g., usual, U-shape, V-shape, etc.).
    /// </summary>
    /// <param name="d">The difference value between two alternatives for a criterion.</param>
    /// <returns>The preference value corresponding to <paramref name="d"/>.</returns>
    protected abstract double ExecuteOne(double d);
}