using LightResults;

namespace McdaToolkit.Serializer.Extensions.JsonSerializer.Errors;

public class NullOrEmptyJsonString : Error
{
    private const string MessageTemplate = "The provided JSON string is null or empty. Ensure that the JSON content is correctly passed and not missing.";

    public NullOrEmptyJsonString() : base(MessageTemplate)
    {
    }
}
