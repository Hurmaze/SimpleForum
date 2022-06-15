using BLL.Models;
using FluentValidation;

namespace BLL.Validation.FluentValidation
{
    public class ForumThreadModelValidator : AbstractValidator<ForumThreadModel>
    {
        public ForumThreadModelValidator()
        {
            RuleFor(t => t.Title)
                .NotNull().NotEmpty()
                .MaximumLength(3)
                .MaximumLength(100);

            RuleFor(t => t.ThemeName)
                .NotNull().NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(t => t.TimeCreated)
                .NotEmpty()
                .InclusiveBetween(DateTime.Parse("2020-10-10"), DateTime.Now)
                .WithMessage("Date time must be valid");
                
        }
    }
}
