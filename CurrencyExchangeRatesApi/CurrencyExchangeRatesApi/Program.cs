using CurrencyExchangeRates.Api.Middleware;
using CurrencyExchangeRates.Application;
using CurrencyExchangeRates.Application.Services;
using CurrencyExchangeRates.Infrastructure;
using CurrencyExchangeRates.Infrastructure.Context;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddDbContext<ExchangeRatesDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<INbpClientService, NbpClientService>((serviceProvider, client) =>
{
    client.BaseAddress = new Uri("https://api.nbp.pl/api/exchangerates/tables/");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new SocketsHttpHandler()
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(15)
    };
})
.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseInMemoryStorage());

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();
builder.Services
    .AddInfrastructure()
    .AddApplication();
var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
// Use Hangfire Dashboard
app.UseHangfireDashboard();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
        builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials()
       );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

