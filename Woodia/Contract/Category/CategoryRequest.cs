namespace Woodia.Contract.Category;

public record CategoryRequest(
    string NameAr,
    string NameEn,
    string SlugAr,
    string SlugEn,
    string ImageThumbnail,
    int ? ParentId);