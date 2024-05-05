using CurrencyExchangeRates.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchangeRates.Application
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IExchangeRatesService, ExchangeRatesService>();

            return services;
        }
    }
}
