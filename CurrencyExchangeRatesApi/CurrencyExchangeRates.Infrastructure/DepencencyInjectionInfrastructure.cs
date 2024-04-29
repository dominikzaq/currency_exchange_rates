using CurrencyExchangeRates.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchangeRates.Infrastructure
{
    public static class DepencencyInjectionInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>()
                .AddScoped<ICurrencyRepository, CurrencyRepository>();

            return services;
        }
    }
}
