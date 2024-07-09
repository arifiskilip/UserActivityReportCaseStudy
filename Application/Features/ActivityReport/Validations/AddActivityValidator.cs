using Application.Features.ActivityReport.Commands.Add;
using FluentValidation;

namespace Application.Features.ActivityReport.Validations
{
    public class AddActivityValidator : AbstractValidator<AddActivityReportCommand>
    {
        public AddActivityValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.ActivityTypeId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);
        }
    }
}
