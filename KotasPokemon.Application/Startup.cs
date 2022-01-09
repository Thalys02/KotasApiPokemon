using AutoMapper;
using KotasPokemon.Application.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace KotasPokemonApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJsonSetup();
            
            services.AddSettingsSetup(Configuration);

            services.AddDependecyInjection(Configuration);

            services.AddDatabaseSetup(Configuration);

            services.AddControllers();

            services.AddAutoMapper(new[] { Assembly.GetExecutingAssembly() });

            services.AddSwaggerSetup();

            services.AddCors();

            services.AddRouting(options => options.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.AddCorsConfig();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.AddSwaggerConfig();

        }
    }
}
