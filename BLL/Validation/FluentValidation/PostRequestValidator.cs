using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class PostRequestValidator : AbstractValidator<PostRequest>
    {
        public PostRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty();
        }
    }
}
