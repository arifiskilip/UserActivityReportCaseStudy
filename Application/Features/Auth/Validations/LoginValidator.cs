using Application.Features.Auth.Commands.Login;
using FluentValidation;

namespace Application.Features.Auth.Validations
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50);

        }
    }
}
