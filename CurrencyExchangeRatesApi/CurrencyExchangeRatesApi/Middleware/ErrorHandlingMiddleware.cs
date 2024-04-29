using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace CurrencyExchangeRates.Api.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception error)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                _logger.LogError(error?.InnerException?.ToString());
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var json = JsonSerializer.Serialize(new { error.Message, error.StackTrace });
                await context.Response.WriteAsync(json);
            }

        }
    }
}
