using FluentValidation;

namespace BlogApp.API.Features.Blog.Favorite
{
    public class FavoriteCommandValidator : AbstractValidator<FavoriteCommand>
    {
        public FavoriteCommandValidator()
        {
            RuleFor(x => x.blogId)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
