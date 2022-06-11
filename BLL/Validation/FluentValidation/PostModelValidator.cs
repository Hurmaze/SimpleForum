using BLL.Models;
using FluentValidation;

namespace BLL.Validation.FluentValidation
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
