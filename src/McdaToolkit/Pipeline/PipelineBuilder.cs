using System.Numerics;
using McdaToolkit.Models.Abstraction;
using McdaToolkit.Pipeline.Steps;

namespace McdaToolkit.Pipeline;

/// <summary>Builds a sequential processing pipeline of MCDA steps.</summary>
public class PipelineBuilder<T> where T : struct, IFloatingPointIeee754<T>
{
    private List<IPipelineStep<T>> _steps = new();
    private McdaMethodAdapter<T> _terminal;

    /// <summary>Appends a pre-processing step to the pipeline.</summary>
    public PipelineBuilder<T> AddPreprocessingStep(IPreProcessingStep<T> step)
    {
        _steps.Add(step);
        return this;
    }
    
    /// <summary>Appends a method step to the pipeline.</summary>
    public PipelineBuilder<T> WithMethod(IMcdaMethod<T> method)
    {
        if (_steps.OfType<McdaMethodAdapter<T>>().Any())
            throw new InvalidOperationException("Pipeline already has a method step.");
        
        _steps.Add(new McdaMethodAdapter<T>(method));
        return this;
    }

    /// <summary>Constructs the pipeline executor from the configured steps.</summary>
    public PipelineExecutor<T> Build()
    {
        var executor = new PipelineExecutor<T>(_steps);
        return executor;
    }
}