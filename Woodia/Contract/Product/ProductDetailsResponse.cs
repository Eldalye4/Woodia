using System.Text.Json.Nodes;
using Woodia.Entity;

namespace Woodia.Contract.Product;

public record ProductDetailsResponse(
    int Id,
    string Category,
    List<string> Images,
    JsonNode ModelConfig
    );