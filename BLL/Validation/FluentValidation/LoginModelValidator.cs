using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class LoginModelValidator : AbstractValidator<LoginRequest>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().NotEmpty()
                .MaximumLength(100)
                .EmailAddress()
                .WithMessage("Maximum 100 characters.");

            RuleFor(x => x.Password)
                .NotNull().NotEmpty()
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$")
                .WithMessage("Password must contain 8-16 characters, at least one letter and one number.");
        }
    }
}
