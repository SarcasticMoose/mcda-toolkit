using LightResults;
using McdaToolkit.Mcda.Providers;

namespace McdaToolkit.Mcda.Methods.Abstraction;

public interface IMcdaMethod<out TResult> : IMcdaMethod
where TResult : IResult<IMcdaScore>
{
    new TResult Run(IDataProvider dataProvider);
}

public interface IMcdaMethod
{
    IResult Run(IDataProvider dataProvider);
}
