using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class ForumThreadModelValidator : AbstractValidator<ForumThreadModel>
    {
        public ForumThreadModelValidator()
        {
            RuleFor(t => t.Title)
                .NotNull().NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(t => t.ThemeName)
                .MaximumLength(50);

            RuleFor(t => t.ThemeId)
                .NotNull().NotEmpty();

            RuleFor(t => t.TimeCreated)
                .NotEmpty()
                .InclusiveBetween(DateTime.Parse("2020-10-10"), DateTime.Now)
                .WithMessage("Date time must be valid");
                
        }
    }
}
