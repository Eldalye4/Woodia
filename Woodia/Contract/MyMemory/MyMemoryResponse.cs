using System.Text.Json.Serialization;

namespace Woodia.Contract.MyMemory;

public record MyMemoryResponse(
    [property: JsonPropertyName("responseData")]
    ResponseData ResponseData);