using FluentValidation;
using Services.Models;

namespace Services.Validation.FluentValidation
{
    public class NicknameModelValidator : AbstractValidator<NicknameModel>
    {
        public NicknameModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MaximumLength(30);
        }
    }
}
