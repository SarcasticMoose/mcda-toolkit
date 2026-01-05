using McdaToolkit.Models.Ranking;

namespace McdaToolkit.Data;

public class ExecutionDetails<T> : ExecutionDetails
    where T : struct, IEquatable<T>, IComparable<T>
{
    public Ranking<T> Ranking {get; internal set;}
    public string Method { get; internal set; }
    public TimeSpan ExecutionTime { get; internal set; }
}

public class ExecutionDetails
{
    public string Method { get; internal set; }
    public TimeSpan ExecutionTime { get; internal set; }
}