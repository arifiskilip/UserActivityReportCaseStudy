using Core.Security.Entitites;

namespace Domain.Entities
{
    public class User : BaseUser
    {
        public bool IsEmailVerified { get; set; } = false;
        public virtual ICollection<ActivityReport> ActivityReports { get; set; }
    }
}
