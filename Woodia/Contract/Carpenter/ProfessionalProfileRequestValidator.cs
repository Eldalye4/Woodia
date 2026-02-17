using FluentValidation;

namespace Woodia.Contract.Carpenter;

public class ProfessionalProfileRequestValidator:AbstractValidator<ProfessionalProfileRequest>
{
    public ProfessionalProfileRequestValidator()
    {
        RuleFor(x=>x.HourlyRate)
            .NotEmpty().WithMessage("HourlyRate is required")
            .InclusiveBetween(20,1000)
            .WithMessage("Hourly rate must be between 20 and 1000");
        RuleFor(x=>x.ProfileOverview)
            .NotEmpty().WithMessage("ProfileOverview is required")
            .Length(50,5000).WithMessage("ProfileOverview must be between 50 and 5000 characters");

        RuleFor(x => x.ProfileImageUrl)
            .NotEmpty().WithMessage("ProfileImage is required");
        



    }
}