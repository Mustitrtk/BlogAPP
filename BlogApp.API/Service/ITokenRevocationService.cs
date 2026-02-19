namespace BlogApp.API.Service
{
    public interface ITokenRevocationService
    {
        void RevokeToken(string token, DateTime expiry);
        bool IsRevoked(string token);
    }
}
