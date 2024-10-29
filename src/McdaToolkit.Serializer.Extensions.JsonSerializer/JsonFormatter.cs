using System.Text.Json;
using LightResults;
using McdaToolkit.Serializer.Abstraction;
using McdaToolkit.Serializer.Extensions.JsonSerializer.Errors;

namespace McdaToolkit.Serializer.Extensions.JsonSerializer;

internal class JsonFormatter : ISerializer
{
    private readonly JsonSerializerOptions? _options;

    public JsonFormatter(JsonSerializerOptions? options)
    {
        _options = options ?? new JsonSerializerOptions();
    }
    
    public Result<T> Deserialize<T>(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return Result.Fail<T>(new NullOrEmptyJsonString());
        }
        try
        {
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<T>(text, _options);
            return deserialized is null ? Result.Fail<T>(new DeserializedObjectIsNull()) : Result.Ok<T>(deserialized);
        }
        catch (JsonException e)
        {
            return Result.Fail<T>(new InvalidJsonText());
        }
    }

    public Result<string> Serialize<T>(T obj)
    {
        if (obj is null)
        {
            return Result<string>.Fail(new ObjectForSerializationNullOrEmpty());
        }
        try
        {
            var serialized = System.Text.Json.JsonSerializer.Serialize(obj, _options);
            return string.IsNullOrEmpty(serialized) ? Result.Fail<string>(new SerializedStringIsEmpty()) : Result.Ok(serialized);
        }
        catch (NotSupportedException ex)
        {
            return Result<string>.Fail(ex.Message);
        }
    }
    
    public string Extension => "json";
}