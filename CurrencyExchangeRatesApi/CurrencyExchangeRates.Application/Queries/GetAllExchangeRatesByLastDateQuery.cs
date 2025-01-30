using CurrencyExchangeRates.Application.Responses;
using MediatR;

namespace CurrencyExchangeRates.Application.Queries
{
    public class GetAllExchangeRatesByLastDateQuery : IRequest<IEnumerable<ExchangeRateResponse>>
    {
    }
}
