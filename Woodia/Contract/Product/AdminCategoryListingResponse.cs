namespace Woodia.Contract.Product;

public record AdminCategoryListingResponse(
    int CategoryId,
    string SlugAr,
    string SlugEn,
    string NameAr,
    string NameEn,
    string ImageThumbnail
    );