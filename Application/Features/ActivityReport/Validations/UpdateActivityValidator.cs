using Application.Features.ActivityReport.Commands.Update;
using FluentValidation;

namespace Application.Features.ActivityReport.Validations
{
    public class UpdateActivityValidator : AbstractValidator<UpdateActivityReportCommand>
    {
        public UpdateActivityValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0);

            RuleFor(x => x.ActivityTypeId)
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .MinimumLength(3)
                .MaximumLength(500);

            RuleFor(x => x.Date)
                .NotNull();

            RuleFor(x => x.Status)
                .NotNull();
        }
    }
}
