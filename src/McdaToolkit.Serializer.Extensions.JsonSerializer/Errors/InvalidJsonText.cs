using LightResults;

namespace McdaToolkit.Serializer.Extensions.JsonSerializer.Errors;

public class InvalidJsonText : Error
{
    private const string MessageTemplate = "The provided JSON document format is invalid. Please ensure the JSON syntax is correct.";

    public InvalidJsonText() : base(MessageTemplate)
    {
    }
}
