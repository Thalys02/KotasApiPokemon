using Microsoft.AspNetCore.Builder;

namespace KotasPokemon.Application.Configurations
{
    public static class CorsConfig
    {
        public static void AddCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors(options => options
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
        }
    }
}
