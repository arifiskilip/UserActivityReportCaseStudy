using Core.Security.Entitites;

namespace Domain.Entities
{
    public class User : BaseUser
    {
        public string? ImageUrl { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public virtual ICollection<ActivityReport> ActivityReports { get; set; }
    }
}
