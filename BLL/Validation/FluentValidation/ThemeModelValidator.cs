using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class ThemeModelValidator : AbstractValidator<ThemeModel>
    {
        public ThemeModelValidator()
        {
            RuleFor(c => c.ThemeName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
