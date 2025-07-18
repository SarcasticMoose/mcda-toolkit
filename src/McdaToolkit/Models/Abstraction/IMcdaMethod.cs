using LightResults;
using McdaToolkit.Data;

namespace McdaToolkit.Models.Abstraction;

public interface IMcdaMethod<T>
{
     IResult<T> Run(McdaInputData data);
}