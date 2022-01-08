using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace KotasPokemon.Application.Configurations
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Kotas Pokemon API",
                        Version = "v1",
                        Description = "Kotas Pokemon API"
                    });
            });

        }


        public static void AddSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("api/v1/swagger.json", "AzureAD_OAuth_API v1");
            });
        }
    }
}
