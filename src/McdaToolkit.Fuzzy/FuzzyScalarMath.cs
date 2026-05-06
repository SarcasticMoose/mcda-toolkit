using McdaToolkit.Types;

namespace McdaToolkit.Fuzzy;

public class FuzzyScalarMath : IScalarMath<TriangularFuzzyNumber>
{
    public TriangularFuzzyNumber One => new(1, 1, 1);
    public TriangularFuzzyNumber Zero => new(0, 0, 0);

    public TriangularFuzzyNumber Add(TriangularFuzzyNumber a, TriangularFuzzyNumber b)
        => new(
            a.L + b.L,
            a.M + b.M,
            a.U + b.U
        );

    public TriangularFuzzyNumber Subtract(TriangularFuzzyNumber a, TriangularFuzzyNumber b)
        => new(
            a.L - b.U,
            a.M - b.M,
            a.U - b.L
        );

    public TriangularFuzzyNumber Multiply(TriangularFuzzyNumber a, TriangularFuzzyNumber b)
        => new(
            a.L * b.L,
            a.M * b.M,
            a.U * b.U
        );

    public TriangularFuzzyNumber Divide(TriangularFuzzyNumber a, TriangularFuzzyNumber b)
        => new(
            a.L / b.U,
            a.M / b.M,
            a.U / b.L
        );

    public TriangularFuzzyNumber Sqrt(TriangularFuzzyNumber value)
        => new(
            Math.Sqrt(value.L),
            Math.Sqrt(value.M),
            Math.Sqrt(value.U)
        );

    public TriangularFuzzyNumber Log(TriangularFuzzyNumber value)
        => new(
            Math.Log(value.L),
            Math.Log(value.M),
            Math.Log(value.U)
        );

    public bool IsZero(TriangularFuzzyNumber value)
        => value.L == 0 && value.M == 0 && value.U == 0;
}