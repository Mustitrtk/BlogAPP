using BlogApp.API.Options;
using MongoDB.Driver;

namespace BlogApp.API.Repository
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddRepositoryExt(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var option = sp.GetRequiredService<MongoOptions>();

                return new MongoClient(option.ConnectionString);
            });

            services.AddScoped(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<MongoOptions>();

                return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
            });

            return services;
        }
    }
}
