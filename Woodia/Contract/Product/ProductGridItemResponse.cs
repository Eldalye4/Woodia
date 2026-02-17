using Woodia.Enums;

namespace Woodia.Contract.Product;

public record ProductGridItemResponse(
    int Id,
    ProductLine ProductLine,
    string Category,
    string  ProductStatus,
    string ImageThumbnail
    );