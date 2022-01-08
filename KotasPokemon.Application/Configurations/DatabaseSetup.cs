using KotasPokemon.Infrastructure.Data;
using KotasPokemon.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KotasPokemon.Application.Configurations
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<PokemonContext>(options =>
            {
                options.UseSqlite("Filename=Pokemon.db");
            });
        }
    }
}
