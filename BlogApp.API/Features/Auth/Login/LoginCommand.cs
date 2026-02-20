using BlogApp.API.Repository;
using BlogApp.API.Service;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Features.Auth.Login
{
    public record LoginCommand(string Username, string Password) : IRequest<LoginResultDto>;

    public record LoginResultDto(string Token, DateTime ExpiresAt);

    public class LoginHandler : IRequestHandler<LoginCommand, LoginResultDto>
    {
        private readonly AppDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly ITokenRevocationService _revocationService;
        private readonly IConfiguration _configuration;
        private readonly Options.JwtSettings _jwtSettings;
        private readonly PasswordHasher<UserEntity> _passwordHasher;

        public LoginHandler(AppDbContext db, ITokenService tokenService, ITokenRevocationService revocationService, IConfiguration configuration, Microsoft.Extensions.Options.IOptions<Options.JwtSettings> jwtOptions)
        {
            _db = db;
            _tokenService = tokenService;
            _revocationService = revocationService;
            _configuration = configuration;
            _jwtSettings = jwtOptions.Value;
            _passwordHasher = new PasswordHasher<UserEntity>();
        }

        public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            if (user == null) throw new Exception("Invalid credentials");

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHashed, request.Password);
            if (verifyResult == PasswordVerificationResult.Failed)
                throw new Exception("Invalid credentials");

            var token = _tokenService.GenerateToken(user);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);

            return new LoginResultDto(token,expires);
        }
    }
}
