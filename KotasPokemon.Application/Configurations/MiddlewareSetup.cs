using KotasPokemon.Application.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace KotasPokemon.Application.Configurations
{
    public static class MiddlewareSetup
    {
        public static void AddMiddlewareSetup(this IServiceCollection services)
        {
            services.AddControllers()
             .ConfigureApiBehaviorOptions(options =>
             {
                 options.SuppressModelStateInvalidFilter = true;
             });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<ValidatorMiddleware>();
            })
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        }
    }
}
