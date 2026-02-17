namespace Woodia.Contract.Product;

public record ProductListingResponse(
    int Id,
    string ProductLine,
    string Description,
    string HoverImage,
    string ThumbnailImage

    );