using BlogApp.API.Features.Auth;

namespace BlogApp.API.Service
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
    }
}
