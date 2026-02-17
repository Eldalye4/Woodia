namespace Woodia.Errors;
public static class CategoryError
{
    public static readonly Error CategoryNotFound = new
    ("Category.CategoryNotFound", "The requested category was not found.", StatusCodes.Status404NotFound);
    public static readonly Error ParentCategoryNotFound = new
        ("Category.ParentNotFound", "The specified parent category was not found.", StatusCodes.Status404NotFound);
    public static readonly Error InvalidParentCategory = new
    ("Category.InvalidParent", "A category cannot be its own parent.", StatusCodes.Status400BadRequest);

}