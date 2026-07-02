using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using LightResults;
using McdaToolkit.Core;
using McdaToolkit.Core.Abstractions;

namespace McdaToolkit.Pipeline;

/// <summary>Extension methods for <see cref="PipelineExecutor{T}"/>.</summary>
[ExcludeFromCodeCoverage]
public static class PipelineExecutorExtensions
{
    /// <inheritdoc cref="PipelineExecutor{T}.Execute{TMcdaResult}(IMcdaMethod{T, TMcdaResult}, ExecutionOptions{T})" />
    public static Result<McdaSolved<T, TMcdaResult>> Execute<T, TMcdaResult>(
        this PipelineExecutor<T> executor,
        IMcdaMethod<T, TMcdaResult> method)
        where T : struct, IFloatingPointIeee754<T>
        where TMcdaResult : IMcdaResult<T>
    {
        return executor.Execute(method, new ExecutionOptions<T>());
    }
}
