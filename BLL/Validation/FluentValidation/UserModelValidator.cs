using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(100)
                .EmailAddress()
                .WithMessage("Maximum 100 characters.");

            RuleFor(x => x.Nickname)
                .MaximumLength(30);
        }
    }
}
