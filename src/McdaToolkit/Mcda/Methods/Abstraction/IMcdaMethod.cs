using LightResults;

namespace McdaToolkit.Mcda.Methods.Abstraction;

public interface IMcdaMethod<out T> : IMcdaMethod
where T : IMcdaScore
{
    new IResult<T> Run(McdaInputData data);
}

public interface IMcdaMethod
{
    IResult<IMcdaScore> Run(McdaInputData data);
}
