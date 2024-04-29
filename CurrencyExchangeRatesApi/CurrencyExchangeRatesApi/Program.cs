using CurrencyExchangeRates.Api.Middleware;
using CurrencyExchangeRates.Application;
using CurrencyExchangeRates.Application.Services;
using CurrencyExchangeRates.Infrastructure;
using CurrencyExchangeRates.Infrastructure.Context;
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

builder.Services
    .AddInfrastructure()
    .AddApplication();

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

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
