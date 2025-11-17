using Microsoft.Extensions.Options;

namespace BlogApp.API.Options
{
    public static class OptionExt
    {
        public static IServiceCollection AddOptionExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOptions>>().Value);

            return services;
        }
    }
}
