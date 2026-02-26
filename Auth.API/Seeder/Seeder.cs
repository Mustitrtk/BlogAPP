using Auth.API.Features.Auth;
using Auth.API.Repository;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Seeder
{
    public static class Seeder
    {
        public static async Task SeedAdminAsync(AppDbContext context)
        {
            var hasher = new PasswordHasher<UserEntity>();
            if (!context.Users.Any())
            {
                var user = new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Role = "Admin"
                };
                user.PasswordHashed = hasher.HashPassword(user, "123456");
                context.Users.Add(user);
            }
        }
    }
}
