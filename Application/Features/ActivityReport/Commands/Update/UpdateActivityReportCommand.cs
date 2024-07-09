using Application.Features.ActivityReport.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ActivityReport.Commands.Update
{
    public class UpdateActivityReportCommand : IRequest<UpdateActivityReportResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        public int ActivityTypeId { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }


        public string[] Roles => [GeneralOperationClaims.User];


        public class UpdateActivityReportCommandHandler : IRequestHandler<UpdateActivityReportCommand, UpdateActivityReportResponse>
        {
            private readonly IMapper _mapper;
            private readonly IActivityReportRepository _activityReportRepository;
            private readonly ActivityReportBusinessRules _businessRules;

            public UpdateActivityReportCommandHandler(IMapper mapper, IActivityReportRepository activityReportRepository, ActivityReportBusinessRules businessRules)
            {
                _mapper = mapper;
                _activityReportRepository = activityReportRepository;
                _businessRules = businessRules;
            }

            public async Task<UpdateActivityReportResponse> Handle(UpdateActivityReportCommand request, CancellationToken cancellationToken)
            {
                var activityReport = await _activityReportRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    include:x=> x.Include(i=> i.ActivityType),
                    enableTracking: true);
                await _businessRules.SelectedEntityIsAvailableAsync(entity: activityReport);
                await _businessRules.CheckActivityTypeIsAvailableAsync(activityTypeId: request.ActivityTypeId);
                await _businessRules.UpdateDuplicatDateCheckAsync(date:request.Date,id:request.Id);
                activityReport = _mapper.Map(request,activityReport);
                await _activityReportRepository.UpdateAsync(entity:activityReport);
                return _mapper.Map<UpdateActivityReportResponse>(activityReport);
            }
        }
    }
}
