using Microsoft.Extensions.DependencyInjection;

using TaxCalculator.Factories;
using TaxCalculator.Interfaces;
using TaxCalculator.Repositories;

namespace TaxCalculator.Helpers
{
    public class ConfigureDependencyInjection
    {
        internal static void InjectDataRepositories(IServiceCollection services)
        {
            services.AddTransient<ITaxCalculationRepository, TaxCalculationRepository>();
        }

        internal static void InjectBusisnessWorkflows(IServiceCollection services)
        {
            services.AddTransient<Workflows.TaxCalculationWorkflow>();
        }

        internal static void InjectFactories(IServiceCollection services)
        {
            services.AddTransient<ITaxCalculatorFactory, TaxCalculatorFactory>();
        }
    }
}
