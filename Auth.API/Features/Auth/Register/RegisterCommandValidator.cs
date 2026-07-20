using FluentValidation;

namespace Auth.API.Features.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(6).WithMessage("{PropertyName} must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
