using System.Text.Json.Nodes;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public record ProductRequest(
    string ProductLine,
    string Description,
    JsonNode ModelConfig,
    List<ProductImageRequest> ProductImage,
    int CategoryId);