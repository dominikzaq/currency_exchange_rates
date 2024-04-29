using CurrencyExchangeRates.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace CurrencyExchangeRates.Application
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IExchangeRatesService, ExchangeRatesService>()
                .AddBackgroundService();

            return services;
        }

        public static IServiceCollection AddBackgroundService(this IServiceCollection services)
        {
            services
                //.AddSingleton<MyJob>()
                .AddSingleton<IJobFactory, JobFactory>()
                .AddSingleton<ISchedulerFactory, StdSchedulerFactory>()
                .AddHostedService<NpbBackgroundService>();

            return services;
        }

    }
}
