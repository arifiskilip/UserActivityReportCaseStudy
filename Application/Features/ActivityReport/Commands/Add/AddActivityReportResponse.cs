namespace Application.Features.ActivityReport.Commands.Add
{
    public class AddActivityReportResponse
    {
        public int Id { get; set; }
        public int ActivityTypeId { get; set; }
        public string? ActivityTypeName { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
