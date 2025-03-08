using LightResults;

namespace McdaToolkit.Mcda.Methods.Abstraction;

public abstract class McdaMethodBase<T> : IMcdaMethod<T> 
    where T : IMcdaScore
{
    public abstract IResult<T> Run(McdaInputData data);

    IResult<IMcdaScore> IMcdaMethod.Run(McdaInputData data)
    {
        return (IResult<IMcdaScore>)Run(data);
    }
}