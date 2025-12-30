using McdaToolkit.Models.Ranking;

namespace McdaToolkit.Data;

public class ExecutionOutput
{
    public Version ToolkitVersion { get; internal set;}
    public string Method { get; internal set; }
    public TimeSpan ExecutionTime {get; internal set;}
    public IRanking Ranking {get; internal set;}
}