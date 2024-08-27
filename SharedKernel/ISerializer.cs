using System.Threading;
using System.Threading.Tasks;
using LightResults;

namespace SharedKernel
{
    public interface ISerializer
    {
        Task<Result<string>> SerializeAsync<T>(T objectToSerialize,CancellationToken ct);
        Task<Result<T>> DeserializeAsync<T>(string textJson,CancellationToken ct);
    }
}