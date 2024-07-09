using Core.Domain;

namespace Application.Features.ActivityReport.Queries.GetAllPaginatedByUserId
{
    public class GetPaginatedActivityReportsByUserIdResponse : IEntity
    {
        public int Id { get; set; }
        public int ActivityTypeId { get; set; }
        public string? ActivityTypeName { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}
