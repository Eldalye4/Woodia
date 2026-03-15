using System.Text.Json.Serialization;

namespace Woodia.Contract.MyMemory;

public record ResponseData(
    [property: JsonPropertyName("translatedText")] 
    string TranslatedText);