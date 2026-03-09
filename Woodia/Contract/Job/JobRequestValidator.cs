using FluentValidation;

namespace Woodia.Contract.Job;

public class JobRequestValidator:AbstractValidator<JobRequest>
{
    public JobRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(20).WithMessage("Description must be at least 20 characters.")
            .MinimumLength(5).WithMessage("Description must be at least 5 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters.")
            .MaximumLength(100).WithMessage("Title must be less than 100 characters.");
        
        RuleFor(x => x.DeliveryDay)
            .GreaterThan(0).WithMessage("DeliveryDay must be at least 1 day.");
        
        RuleFor(x => x.ExpectedBudget)
            .GreaterThan(0).WithMessage("ExpectedBudget must be at least 1 day.");
    }
}

