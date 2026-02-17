using System.Text.Json.Nodes;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public record ProductResponse(
    int Id,
    string ProductLine,
    string Description,
    string ProductStatus,
    JsonNode ModelConfig,
    List<ProductImageResponse> ProductImageResponses,
    CategoryProductResponse  CategoryProductResponses);
