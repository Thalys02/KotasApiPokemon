using FluentValidation.AspNetCore;
using KotasPokemonApi;
using Microsoft.Extensions.DependencyInjection;

namespace KotasPokemon.Application.Configurations
{
    public static class FluentValidationSetup
    {
        public static void AddFluentValidationSetup(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
        }
    }
}
