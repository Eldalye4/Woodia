namespace Woodia.Contract.Category;

public record AdminCategoryResponse(
    int Id,
    string SlugAr,
    string SlugEn,
    string NameAr,
    string NameEn,
    string ImageThumbnail,
    CategoryListingForAdminResponse? ParentCategory
    );