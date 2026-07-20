using Auth.API.Repository;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IResult>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public RegisterCommandHandler(AppDbContext context, IPasswordHasher<UserEntity> passwordHasher)
        {
            _appDbContext = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<IResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool isUserNameExist = await _appDbContext.Users
                    .AnyAsync(x => x.Username == request.username, cancellationToken);

                if (isUserNameExist)
                {
                    return Results.BadRequest("Username already exists.");
                }

                var user = new UserEntity
                {
                    Id = NewId.NextSequentialGuid(),
                    Username = request.username,
                };

                user.PasswordHashed = _passwordHasher.HashPassword(user, request.password);

                await _appDbContext.Users.AddAsync(user, cancellationToken);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return Results.Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return Results.Problem("An error occurred while processing your request.");
            }
        }
    }
}