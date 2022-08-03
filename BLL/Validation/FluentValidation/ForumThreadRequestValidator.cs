using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class ForumThreadRequestValidator : AbstractValidator<ForumThreadRequest>
    {
        public ForumThreadRequestValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(t => t.Content)
                .NotEmpty();
        }
    }
}
