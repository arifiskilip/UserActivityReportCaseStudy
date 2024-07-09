namespace Application.Features.Auth.Queries.IsEmailVerified
{
    public class IsEmailVerifiedResponse
    {
        public int UserId { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
