using System.Diagnostics;
using LightResults;
using McdaToolkit.Data;
using McdaToolkit.Models.Ranking;

namespace McdaToolkit.Models.Abstraction;

public abstract class McdaMethod<T> : IMcdaMethod<T>
    where T : struct, IEquatable<T>, IComparable<T>
{
    public abstract string Name { get; }

    /// <inheritdoc cref="IMcdaMethod{T}.Run"/>
    public IResult<ExecutionDetails<T>> Run(McdaInputData data, McdaExecutionOptions? options = null)
    {
        options ??= new McdaExecutionOptions();

        var startTimestamp = Stopwatch.GetTimestamp();
        var executionOutput = Execute(data);
        var endTimestamp = Stopwatch.GetTimestamp();

        var executionDetails = new ExecutionDetails<T>()
        {
            ExecutionTime = GetExecutionTime(startTimestamp, endTimestamp),
            Ranking = executionOutput.CreateRanking(options.RankingOptions),
            Method = Name
        };
        
        return Result.Success(executionDetails);
    }

    internal abstract IEnumerable<T> Execute(McdaInputData data);

    private TimeSpan GetExecutionTime(
        long startTimestamp,
        long endTimestamp)
    {
        var elapsedTicks = endTimestamp - startTimestamp;
        var elapsedSecondsInStopwatchNativeFrequencyTicks = ((double)elapsedTicks / Stopwatch.Frequency);
        var elapsedSecondsInTimeSpanTicks = (long)(elapsedSecondsInStopwatchNativeFrequencyTicks * TimeSpan.TicksPerSecond);
        return TimeSpan.FromTicks(elapsedSecondsInTimeSpanTicks);
    }
}
