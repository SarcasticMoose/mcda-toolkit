using LightResults;

namespace McdaToolkit.Mcda.Methods.Abstraction;

public interface IMcdaMethod<out T>
{
     IResult<T> Run(McdaInputData data);
}