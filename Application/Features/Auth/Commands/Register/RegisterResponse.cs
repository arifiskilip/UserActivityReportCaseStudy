using Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterResponse
    {
        public AccessToken AccessToken { get; set; }
    }
}
