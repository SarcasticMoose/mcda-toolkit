using System.Numerics;

namespace McdaToolkit.Pipeline;

/// <summary>Options that control execution</summary>
public record ExecutionOptions<TType>
    where TType : struct, IFloatingPointIeee754<TType>
{
}
