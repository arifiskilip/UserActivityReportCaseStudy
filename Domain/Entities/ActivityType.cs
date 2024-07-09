using Core.Domain;

namespace Domain.Entities
{
    public class ActivityType : Entity<int>
    {
        public string? Name { get; set; }


        public virtual ICollection<ActivityReport> ActivityReports { get; set; }
    }
}
