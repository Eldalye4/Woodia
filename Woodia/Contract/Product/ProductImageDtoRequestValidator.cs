using FluentValidation;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public class ProductImageDtoRequestValidator:AbstractValidator<ProductImageDto>
{
    public ProductImageDtoRequestValidator()
    {
        RuleFor(x => x.ImagePlace)
            .IsEnumName(typeof(ImagePlace), false)
            .WithMessage(
                "Invalid ImagePlace value. The allowed values are: Thumbnail, Hover, Gallery");
    }
}