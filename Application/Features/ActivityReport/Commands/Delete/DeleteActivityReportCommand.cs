using Application.Features.ActivityReport.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;

namespace Application.Features.ActivityReport.Commands.Delete
{
    public class DeleteActivityReportCommand : IRequest<DeleteActivityReportResponse>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];


        public class DeleteActivityReportCommandHandler : IRequestHandler<DeleteActivityReportCommand, DeleteActivityReportResponse>
        {
            private readonly IActivityReportRepository _activityReportRepository;
            private readonly ActivityReportBusinessRules _businessRules;

            public DeleteActivityReportCommandHandler(IActivityReportRepository activityReportRepository, ActivityReportBusinessRules businessRules)
            {
                _activityReportRepository = activityReportRepository;
                _businessRules = businessRules;
            }

            public async Task<DeleteActivityReportResponse> Handle(DeleteActivityReportCommand request, CancellationToken cancellationToken)
            {
                var activityReport = await _activityReportRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    enableTracking: true);
                await _businessRules.SelectedEntityIsAvailableAsync(entity: activityReport);
                await _activityReportRepository.DeleteAsync(entity: activityReport);
                return new()
                {
                    Message = "Silme işlemi başarılı!"
                };
            }
        }
    }
}
