using FluentValidation;

namespace Woodia.Contract.Client;

public class ImageProfileRequestValidator:AbstractValidator<ImageProfileRequest>
{
    public ImageProfileRequestValidator()
    {
        RuleFor(x=>x.photoUrl)
            .NotEmpty().WithMessage("Photo UrL is Required.");
    }
}