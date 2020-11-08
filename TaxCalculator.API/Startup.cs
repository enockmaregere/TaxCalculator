using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TaxCalculator.DataContext;
using TaxCalculator.Helpers;
using TaxCalculator.RESTAPI.Middleware;

namespace TaxCalculator.API
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
            ConfigureDependencyInjection.InjectDataRepositories(services);
            ConfigureDependencyInjection.InjectBusisnessWorkflows(services);
            ConfigureDependencyInjection.InjectFactories(services);

            services.AddCors();

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<TaxCalculatorContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TaxCalculatorConnectionString"), options => options.EnableRetryOnFailure()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(
                options => options.WithOrigins("https://localhost:44344").AllowAnyMethod());

            app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
