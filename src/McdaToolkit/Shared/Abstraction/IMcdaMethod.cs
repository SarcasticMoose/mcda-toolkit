using LightResults;
using McdaToolkit.Shared.Data;

namespace McdaToolkit.Shared.Abstraction;

public interface IMcdaMethod<out T>
{
     IResult<T> Run(McdaInputData data);
}