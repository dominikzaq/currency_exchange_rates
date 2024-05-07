using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CurrencyExchangeRates.Application
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBackgroundJob, BackgroundJob>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
