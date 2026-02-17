namespace Woodia.Contract.Category;

public record CategoryRequest(
    string Name,
    string Slug,
    string ImageThumbnail,
    int ? ParentId);