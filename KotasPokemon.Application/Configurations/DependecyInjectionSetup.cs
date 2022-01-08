using KotasPokemon.Domain.Core.Interfaces;
using KotasPokemon.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KotasPokemon.Application.Configurations
{
    public static class DependecyInjectionSetup
    {
        public static void AddDependecyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPokemonRepository, PokemonRepository>();
        }
    }
}
