namespace Application.Features.ActivityReport.Commands.Update
{
    public class UpdateActivityReportResponse
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
