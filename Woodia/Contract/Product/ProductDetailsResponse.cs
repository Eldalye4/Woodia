using System.Text.Json.Nodes;
using Woodia.Entity;

namespace Woodia.Contract.Product;

public record ProductDetailsResponse(
    int Id,
    CategorySummaryResponse Category,
    List<string> Images,
    JsonNode ModelConfig
    );