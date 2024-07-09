using Application.Features.Auth.Commands.EmailVerified;
using FluentValidation;

namespace Application.Features.Auth.Validations
{
    public class EmailVerifiedValidator : AbstractValidator<EmailVerifiedCommand>
    {
        public EmailVerifiedValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .Length(6);
        }
    }
}
