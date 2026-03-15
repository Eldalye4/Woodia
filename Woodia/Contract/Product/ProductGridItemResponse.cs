using Woodia.Enums;

namespace Woodia.Contract.Product;

public record ProductGridItemResponse(
    int Id,
    string ProductLine,
    string Category,
    string  ProductStatus,
    string ImageThumbnail
    );