using FluentValidation;

namespace Woodia.Contract.Offer;

public class OfferRequestValidator:AbstractValidator<OfferRequest>
{
    public OfferRequestValidator()
    {
        RuleFor(x=>x.DeliveryDay)
            .GreaterThan(0).WithMessage("DeliveryDay must be greater than 0");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(20).WithMessage("Description must be at least 20 characters.")
            .MinimumLength(5).WithMessage("Description must be at least 5 characters.");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}