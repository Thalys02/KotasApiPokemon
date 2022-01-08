using KotasPokemon.Infrastructure.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KotasPokemon.Application.Configurations
{
    public static class AppSettingsSetup
    {
        public static void AddSettingsSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PokeApiSettings>(configuration.GetSection("PokeApiSettings"));
        }
    }
}
