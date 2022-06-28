using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class AccountModelValidator : AbstractValidator<AccountModel>
    {
        public AccountModelValidator()
        {
            RuleFor(a => a.Email)
                .NotNull().NotEmpty()
                .MaximumLength(100)
                .EmailAddress()
                .WithMessage("Maximum 100 characters.");

            RuleFor(a => a.PasswordHash).NotNull().NotEmpty();

            RuleFor(a => a.PasswordSalt).NotNull().NotEmpty();

            RuleFor(a => a.RoleId).NotNull().NotEmpty();
        }
    }
}
