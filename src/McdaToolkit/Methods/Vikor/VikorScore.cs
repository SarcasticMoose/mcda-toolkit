namespace McdaToolkit.Methods.Vikor;

public record struct VikorScore : IComparable<VikorScore>
{
    public double S { get; set; }
    public double R { get; set; }
    public double Q { get; set; }

    public int CompareTo(VikorScore other)
    {
        return Q.CompareTo(other.Q);
    }
}