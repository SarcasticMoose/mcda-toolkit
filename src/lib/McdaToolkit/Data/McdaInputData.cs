using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Data;

/// <summary>
/// Represents the input data for a Multi-Criteria Decision Analysis (MCDA) method.
/// Contains the decision matrix, criteria weights, and types of criteria
/// </summary>
public record McdaInputData(
    Matrix<double> Matrix,
    Vector<double> Weights,
    int[] Types)
{
    /// <summary>
    /// Gets the array indicating the type of each criterion.
    /// A value of 1 indicates a benefit criterion (to be maximized),
    /// and -1 a cost criterion (to be minimized).
    /// </summary>
    public int[] Types { get; } = Types;
    
    /// <summary>
    /// Gets the decision matrix, where rows represent alternatives and columns represent criteria.
    /// </summary>
    public Matrix<double> Matrix { get; } = Matrix;
    
    /// <summary>
    /// Gets the weight vector indicating the relative importance of each criterion.
    /// </summary>
    public Vector<double> Weights { get; } = Weights;
}