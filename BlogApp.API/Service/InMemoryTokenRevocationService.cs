
using System.Collections.Concurrent;

namespace BlogApp.API.Service
{
    public class InMemoryTokenRevocationService : ITokenRevocationService
    {
        private readonly ConcurrentDictionary<string, DateTime> _revoked = new();
        public bool IsRevoked(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            if (_revoked.TryGetValue(token, out var expiry))
            {
                if (expiry < DateTime.UtcNow)
                {
                    _revoked.TryRemove(token, out _);
                    return false;
                }

                return true;
            }

            return false;
        }

        public void RevokeToken(string token, DateTime expiry)
        {
            _revoked[token] = expiry;
        }
    }
}
