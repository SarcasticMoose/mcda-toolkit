using LightResults;
using MathNet.Numerics.LinearAlgebra;

namespace McdaToolkit.Mcda.Methods.Abstraction;

public interface ICalculation<TValue> where TValue : struct, IEquatable<TValue>, IFormattable
{
    /// <summary>
    /// Calculate provided data
    /// </summary>
    /// <param name="matrix">Data as set of alternatives and theirs attributes</param>
    /// <param name="weights">Data determining relevance of each attribute</param>
    /// <param name="criteriaDirections">Data that determines the columns is profit or cost</param>
    /// <returns>Vector of processed data in descending order</returns>
    Result<Vector<TValue>> Calculate(IEnumerable<IEnumerable<TValue>> matrix, IEnumerable<TValue> weights, IEnumerable<int> criteriaDirections);
}

