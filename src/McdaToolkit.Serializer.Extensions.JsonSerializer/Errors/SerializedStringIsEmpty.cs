using LightResults;

namespace McdaToolkit.Serializer.Extensions.JsonSerializer.Errors;

public class SerializedStringIsEmpty : Error
{
    private const string MessageTemplate = "The serialized string is empty. Verify that the object contains data and that the serialization process completed successfully.";

    public SerializedStringIsEmpty() : base(MessageTemplate)
    {
    }
}
