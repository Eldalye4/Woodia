namespace Woodia.Contract.Category;

public record AdminCategoryResponse(
    int Id,
    string Slug,
    string Name,
    string ImageThumbnail,
    ParentCategoryResponse? ParentCategory
    );