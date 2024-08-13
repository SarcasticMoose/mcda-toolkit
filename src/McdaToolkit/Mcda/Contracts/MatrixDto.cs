namespace McdaToolkit.Mcda.Contracts;

internal record MatrixDto(double[,] Matrix, double[] Weights, int[] CriteriaDecision)
{
    public double[,] Matrix { get; } = Matrix;
    public double[] Weights { get; } = Weights;
    public int[] CriteriaDecision { get; } = CriteriaDecision;
}