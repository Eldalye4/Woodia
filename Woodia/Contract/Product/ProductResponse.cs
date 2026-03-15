using System.Text.Json.Nodes;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public record ProductResponse(
    int Id,
    string ProductLineAr,
    string ProductLineEn,
    string DescriptionAr,
    string DescriptionEn,
    string ProductStatus,
    JsonNode ModelConfig,
    List<ProductImageResponse> ProductImageResponses,
    CategoryProductResponse  CategoryProductResponses);
