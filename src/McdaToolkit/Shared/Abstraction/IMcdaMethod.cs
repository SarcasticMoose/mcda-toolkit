using LightResults;
using McdaToolkit.Shared.Data;

namespace McdaToolkit.Shared.Abstraction;

<<<<<<< HEAD
public interface IMcdaMethod<T>
=======
public interface IMcdaMethod<out T>
>>>>>>> cc9253a (feat: updated namespaces)
{
     IResult<T> Run(McdaInputData data);
}