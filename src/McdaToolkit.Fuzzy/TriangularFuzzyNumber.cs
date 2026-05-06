namespace McdaToolkit.Fuzzy;

public readonly record struct TriangularFuzzyNumber(double L, double M, double U) : IFormattable
{
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        FormattableString formattable = $"{nameof(L)}: {L}, {nameof(M)}: {M}, {nameof(U)}: {U}";
        return formattable.ToString(formatProvider);
    }

    public readonly override string ToString()
    {
        return $"{nameof(L)}: {L}, {nameof(M)}: {M}, {nameof(U)}: {U}";
    }

    public double L { get; } = L;
    public double M { get; } = M;
    public double U { get; } = U;
    
    public static TriangularFuzzyNumber operator *(TriangularFuzzyNumber a, TriangularFuzzyNumber b) => new(a.L * b.L, a.M * b.M, a.U * b.U);
    
    public static TriangularFuzzyNumber operator /(TriangularFuzzyNumber a, TriangularFuzzyNumber b)
    {
        if (b.L <= 0)
            throw new ArgumentException("Division by fuzzy number containing zero is undefined.");

        return new TriangularFuzzyNumber(
            a.L / b.U,
            a.M / b.M,
            a.U / b.L
        );
    }
}