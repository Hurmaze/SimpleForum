using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class PostModelValidator : AbstractValidator<PostModel>
    {
        public PostModelValidator()
        {
            RuleFor(x => x.Content)
                .NotNull().NotEmpty();
        }
    }
}
