using Auth.API.Features.Auth;
using Auth.API.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Seeder
{
    public static class Seeder
    {
        public static async Task SeedAdminAsync(AppDbContext context)
        {
            try
            {
                var hasher = new PasswordHasher<UserEntity>();

                // Veritabanının ve koleksiyonun oluşturulduğundan emin ol (EF Core Mongo için önemli)
                // await context.Database.EnsureCreatedAsync(); 

                var anyUser = await context.Users.AnyAsync(); // Listleyip RAM'i yormaktansa AnyAsync daha hızlıdır
                if (!anyUser)
                {
                    var user = new UserEntity()
                    {
                        Id = Guid.NewGuid(),
                        Username = "admin",
                        Role = "Admin"
                    };
                    user.PasswordHashed = new PasswordHasher<UserEntity>().HashPassword(user,"Admin_12345");

                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync(); 
                    Console.WriteLine("Seeder: Admin kullanıcısı oluşturuldu.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeder Hatası: {ex.Message}");
            }
        }
    }
}
