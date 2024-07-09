﻿namespace Application.Features.ActivityReport.Queries.GetById
{
    public class GetByIdActivityReportResponse
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
