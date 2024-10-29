using LightResults;

namespace McdaToolkit.Serializer.Extensions.JsonSerializer.Errors;

public class DeserializedObjectIsNull : Error
{
    private const string MessageTemplate = "Deserialization failed: the deserialized object is null. Ensure the input data format is correct and matches the expected type.";

    public DeserializedObjectIsNull() : base(MessageTemplate)
    {
    }
}
