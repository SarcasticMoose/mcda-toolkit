using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Services.MatrixNormalizator;

namespace McdaToolkit.Normalization.Services.Abstraction;

internal interface IMatrixNormalizationService<TInput, TOutput> 
    where TInput : struct, IEquatable<TInput>, IFormattable
    where TOutput : struct, IEquatable<TOutput>, IFormattable
{
    Matrix<TOutput> NormalizeMatrix(
        Matrix<TInput> matrix, 
        CriterionType[] criteriaTypes);
}