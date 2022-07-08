using FluentValidation;
using Services.Models;

namespace Services.Validation.FluentValidation
{
    public class NicknameModelValidator : AbstractValidator<NicknameRequest>
    {
        public NicknameModelValidator()
        {
            RuleFor(x => x.Nickname)
                .NotNull()
                .MaximumLength(30);
        }
    }
}
