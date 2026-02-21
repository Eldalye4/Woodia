using System.Text.Json.Nodes;

namespace Woodia.Contract.Product;

public record ProductUpdateRequest(
    string ProductLine,
    string Description,
    JsonNode ModelConfig,
    List<ProductImageDto> ProductImage,
    int CategoryId);


