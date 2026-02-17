using FluentValidation;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public class ProductImageRequestValidator:AbstractValidator<ProductImageRequest>
{
    public ProductImageRequestValidator()
    {
        RuleFor(x => x.imagePlace)
            .IsEnumName(typeof(ImagePlace), false)
            .WithMessage(
                "Invalid ImagePlace value. The allowed values are: Thumbnail, Hover, Gallery");
    }
}