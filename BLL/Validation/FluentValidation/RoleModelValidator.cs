using BLL.Models;
using FluentValidation;

namespace BLL.Validation.FluentValidation
{
    public class RoleModelValidator : AbstractValidator<RoleModel>
    {
        public RoleModelValidator()
        {
            RuleFor(x => x.RoleName)
                .NotNull().NotEmpty()
                .MaximumLength(50);
               
        }
    }
}
