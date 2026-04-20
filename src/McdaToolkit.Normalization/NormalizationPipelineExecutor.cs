using MathNet.Numerics.LinearAlgebra;
using McdaToolkit.Normalization.Methods.Abstraction;

namespace McdaToolkit.Normalization;

public class NormalizationPipelineExecutor<T> where T : struct, IEquatable<T>, IFormattable
{
    private readonly IVectorNormalizer<T> _normalizer;
    private readonly ITransformerRegistry<T> _transformerRegistry;

    public NormalizationPipelineExecutor(
        IVectorNormalizer<T> normalizer,
        ITransformerRegistry<T> transformerRegistry)
    {
        _normalizer = normalizer;
        _transformerRegistry = transformerRegistry;
    }

    public Matrix<T> Process(
        McdaProblem<T> mcdaProblem)
    {
        var rows = mcdaProblem.Data.RowCount;
        var cols = mcdaProblem.Data.ColumnCount;

        var result = Matrix<T>.Build.Dense(rows, cols);
        for (int j = 0; j < cols; j++)
        {
            var column = mcdaProblem.Data.Column(j);
            var transformer = _transformerRegistry.Get(mcdaProblem.Criteria[j].Type);
            var transformed = transformer.Transform(column);
            var normalized = _normalizer.Normalize(transformed);
            for (int i = 0; i < rows; i++)
            {
                result[i, j] = normalized[i];
            }
        }
        return result;
    }
}