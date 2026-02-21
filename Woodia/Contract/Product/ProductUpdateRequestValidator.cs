using FluentValidation;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public class ProductUpdateRequestValidator:AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(x => x.ProductLine)
            .IsEnumName(typeof(ProductLine), caseSensitive: false)
            .WithMessage(
                "Invalid ProductLine value. The allowed values are: OriginalClassic, OriginalModern, Edge, Tone.");
        RuleForEach(x=>x.ProductImage)
            .SetValidator(new ProductImageDtoRequestValidator());

        RuleFor(x => x.ProductImage)
            .Must(image => image.Count(x =>
                string.Equals(x.ImagePlace, nameof(ImagePlace.Hover), StringComparison.OrdinalIgnoreCase)) <= 1
            ).WithMessage("You can only have one Hover image.")
            .Must(image=>image.Count(
                x=>string.Equals(x.ImagePlace, nameof(ImagePlace.Thumbnail), StringComparison.OrdinalIgnoreCase))<=1
            ).WithMessage("You can only have one Thumbnail image.")
            .Must(image=>image.Count(
                x=>string.Equals(x.ImagePlace, nameof(ImagePlace.Thumbnail), StringComparison.OrdinalIgnoreCase))==1
            ).WithMessage("Thumbnail image is required.");
    }
}