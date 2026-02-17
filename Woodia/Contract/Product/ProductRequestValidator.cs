using FluentValidation;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public class ProductRequestValidator:AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.ProductLine)
            .IsEnumName(typeof(ProductLine), caseSensitive: false)
            .WithMessage(
                "Invalid ProductLine value. The allowed values are: OriginalClassic, OriginalModern, Edge, Tone.");
        RuleForEach(x=>x.ProductImage)
            .SetValidator(new ProductImageRequestValidator());

        RuleFor(x => x.ProductImage)
            .Must(image => image.Count(x =>
                string.Equals(x.imagePlace, nameof(ImagePlace.Hover), StringComparison.OrdinalIgnoreCase)) <= 1
            ).WithMessage("You can only have one Hover image.")
            .Must(image=>image.Count(
                x=>string.Equals(x.imagePlace, nameof(ImagePlace.Thumbnail), StringComparison.OrdinalIgnoreCase))<=1
                ).WithMessage("You can only have one Thumbnail image.")
            .Must(image=>image.Count(
                x=>string.Equals(x.imagePlace, nameof(ImagePlace.Thumbnail), StringComparison.OrdinalIgnoreCase))==1
            ).WithMessage("Thumbnail image is required.");
        
    }
}