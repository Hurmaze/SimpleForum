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
                .MaximumLength(100);

            RuleFor(t => t.ThemeName)
                .NotNull().NotEmpty()
                .MaximumLength(50);

            RuleFor(t => t.TimeCreated)
                .NotEmpty();
                
        }
    }
}
