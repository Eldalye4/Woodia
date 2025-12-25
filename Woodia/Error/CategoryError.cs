public static class CategoryError
{
    public static readonly Error CategoryNotFound = new
    ("Category.CategoryNotFound", "The requested category was not found.", StatusCodes.Status404NotFound);

}