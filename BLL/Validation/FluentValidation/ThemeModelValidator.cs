using BLL.Models;
using FluentValidation;

namespace BLL.Validation.FluentValidation
{
    public class ThemeModelValidator : AbstractValidator<ThemeModel>
    {
        public ThemeModelValidator()
        {
            RuleFor(c => c.ThemeName)
                .NotNull().NotEmpty()
                .MaximumLength(50);
        }
    }
}
