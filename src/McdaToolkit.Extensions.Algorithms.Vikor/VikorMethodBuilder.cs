using System.Diagnostics.CodeAnalysis;
using McdaToolkit.Core.Abstractions;
using McdaToolkit.Extensions.Algorithms.Vikor.Result;

namespace McdaToolkit.Extensions.Algorithms.Vikor;

/// <summary>
/// Builder for creating <see cref="VikorMethod{T}"/> instances.
/// </summary>
/// <typeparam name="T">The type of the result values.</typeparam>
[ExcludeFromCodeCoverage]
public sealed class VikorMethodBuilder<T>
    where T : struct, System.Numerics.IFloatingPointIeee754<T>
{
    private VikorMethodParameters<T>? _vikorMethodParameters;

    /// <summary>
    /// Configures the parameters for the Vikor method.
    /// </summary>
    /// <param name="configure">An action to configure the parameters.</param>
    /// <returns>The current builder instance.</returns>
    public VikorMethodBuilder<T> WithParameters(Action<VikorMethodParametersBuilder<T>> configure)
    {
        var builder = new VikorMethodParametersBuilder<T>();
        configure(builder);
        _vikorMethodParameters = builder.Build();
        return this;
    }

    /// <summary>
    /// Builds the Vikor method instance.
    /// </summary>
    /// <returns>The Vikor method instance.</returns>
    public IMcdaMethod<T,VikorMethodResult<T>> Build()
    {
        return new VikorMethod<T>(
            _vikorMethodParameters ?? VikorMethodParameters<T>.Default
        );
    }
}
