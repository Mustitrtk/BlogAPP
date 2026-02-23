using Auth.API.Features.Auth;

namespace Auth.API.Service
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
    }
}
