using FluentValidation;
using Woodia.Enums;

namespace Woodia.Contract.Product;

public class ProductStatusRequestValidator:AbstractValidator<ProductStatusRequest>
{
    public ProductStatusRequestValidator()
    {
        RuleFor(p => p.Status)
            .NotEmpty()
            .IsEnumName(typeof(ProductStatus), caseSensitive: false)
                .WithMessage(
                    "Invalid ProductStatus value. The allowed values are: Draft, Published.");
    }
}