using Core.Domain;

namespace Domain.Entities
{
    public class ActivityReport : Entity<int>
    {
        public int UserId { get; set; }
        public int ActivityTypeId { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }


        public User User { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
