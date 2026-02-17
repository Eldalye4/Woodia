namespace Woodia.Contract.Product;

public record AdminCategoryListingResponse(
    int CategoryId,
    string Slug,
    string CategoryName,
    string ImageThumbnail
    );