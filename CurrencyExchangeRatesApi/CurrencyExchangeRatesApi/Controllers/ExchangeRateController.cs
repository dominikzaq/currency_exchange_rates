using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace CurrencyExchangeRates.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRatesService _exchangeRatesService;

        public ExchangeRateController(IExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all current exchange rates")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ExchangeRateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ExchangeRateDto>> GetCurrentExchangeRates()
        {
            var result = await _exchangeRatesService.GetAllByLastDateAsync();

            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add if no actual and get exchange rates ")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ExchangeRateDto>> UpdateExchangeRates()
        {
            await _exchangeRatesService.AddIfNoExistsAsync();

            var result = await _exchangeRatesService.GetAllByLastDateAsync();

            return Ok(result);
        }
    }
}
