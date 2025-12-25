public record CategoryResponse(
    string Name,
    string Slug,
    List<CategoryChildResponse> ChildCategory
);