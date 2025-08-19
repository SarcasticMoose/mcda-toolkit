namespace McdaToolkit.Models.Rankings;

/// <summary>
/// Represents a single row in a ranking of MCDA alternatives,
/// including the alternative's index, its rank, and its score or evaluation value.
/// </summary>
/// <typeparam name="T">
/// The type of the score assigned to the alternative
/// </typeparam>
public record struct RankingRow<T>
    where T : struct, IComparable<T>, IEquatable<T>
{
    internal RankingRow(
        int alternative, 
        int rank, 
        T score)
    {
        Alternative = alternative;
        Rank = rank;
        Score = score;
    }
    
    /// <summary>
    /// Gets the index of the alternative in the original input list
    /// </summary>
    public int Alternative { get; }
    
    /// <summary>
    /// Gets the rank position of the alternative. A lower number means a higher position in the ranking.
    /// </summary>
    public int Rank { get; }
    
    /// <summary>
    /// Gets the score or preference value assigned to the alternative.
    /// </summary>
    public T Score { get; }
}
