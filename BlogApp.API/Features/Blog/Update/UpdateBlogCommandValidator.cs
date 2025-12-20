using FluentValidation;

namespace BlogApp.API.Features.Blog.Update
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.AuthorName)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
