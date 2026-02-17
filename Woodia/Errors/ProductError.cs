
public static class ProductError
{
    public static readonly Error ProductNotFound =
    new Error("Product.NotFound", "No Product was found with the given ID", StatusCodes.Status404NotFound);
}