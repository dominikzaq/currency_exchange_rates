﻿using CurrencyExchangeRates.Application.Commands;
using CurrencyExchangeRates.Application.Queries;
using CurrencyExchangeRates.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace CurrencyExchangeRates.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExchangeRateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get latest exchange rates")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ExchangeRateResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<ExchangeRateResponse>>> GetLatestExchangeRates()
        {
            var query = new GetAllExchangeRatesByLastDateQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Update exchange rates")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ExchangeRateResponse>> UpdateExchangeRates()
        {
            var updateExchangeRateCommand = new UpdateExchangeRateCommand();
            bool result = await _mediator.Send(updateExchangeRateCommand);

            return Ok(result);
        }
    }
}
