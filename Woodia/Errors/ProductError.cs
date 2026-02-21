public static class ProductError
{
    public static readonly Error ProductNotFound =
    new Error("Product.NotFound", "No Product was found with the given ID", StatusCodes.Status404NotFound);
    public static readonly Error CannotLinkToParent = new(
        "Category.CannotLinkToParent", 
        "A product cannot be linked directly to a parent category.", 
        StatusCodes.Status400BadRequest);
}