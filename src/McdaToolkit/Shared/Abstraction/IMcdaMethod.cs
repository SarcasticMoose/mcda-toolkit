using LightResults;
using McdaToolkit.Shared.Data;

namespace McdaToolkit.Shared.Abstraction;

public interface IMcdaMethod<T>
{
     IResult<T> Run(McdaInputData data);
}