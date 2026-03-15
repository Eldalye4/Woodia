using System.Text.Json.Nodes;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public record ProductRequest(
    string ProductLine,
    string DescriptionAr,
    string DescriptionEn,
    JsonNode ModelConfig,
    List<ProductImageRequest> ProductImage,
    int CategoryId);