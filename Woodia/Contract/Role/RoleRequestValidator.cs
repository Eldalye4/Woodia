using FluentValidation;

namespace Woodia.Contract.Role;

public class RoleRequestValidator:AbstractValidator<RoleRequest>
{
    public RoleRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .Length(3, 200);
        RuleFor(r => r.Permissions)
            .NotNull()
            .NotEmpty();
        RuleFor(x=>x.Permissions)
            .Must(x=>x.Distinct().Count() == x.Count())
            .WithMessage("You cannot and duplicated permissions for the same role")
            .When(x=>x.Permissions !=null);
    }
}