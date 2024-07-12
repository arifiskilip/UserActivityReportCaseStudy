namespace Application.Features.ActivityType.Queries.GetAll
{
    public class GetAllActivityTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; } 
    }
}
