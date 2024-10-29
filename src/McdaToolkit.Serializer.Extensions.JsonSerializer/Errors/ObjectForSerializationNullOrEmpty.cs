using LightResults;

namespace McdaToolkit.Serializer.Extensions.JsonSerializer.Errors;

public class ObjectForSerializationNullOrEmpty : Error
{
    private const string MessageTemplate = "The object provided for serialization is null or empty. Ensure the object is initialized and contains data before serialization.";

    public ObjectForSerializationNullOrEmpty() : base(MessageTemplate)
    {
    }
}
