using CurrencyExchangeRates.Application.Mapper;
using CurrencyExchangeRates.Application.Queries;
using CurrencyExchangeRates.Application.Responses;
using CurrencyExchangeRates.Infrastructure.Repositories;
using MediatR;

namespace CurrencyExchangeRates.Application.Handlers
{
    public class GetAllExchangeRatesByLastDateHandler : IRequestHandler<GetAllExchangeRatesByLastDateQuery, IEnumerable<ExchangeRateResponse>>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public GetAllExchangeRatesByLastDateHandler(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<IEnumerable<ExchangeRateResponse>> Handle(GetAllExchangeRatesByLastDateQuery request, CancellationToken cancellationToken)
        {
            var exchangeRatesActual = await _exchangeRateRepository.GetLastOrderByDateAsync();

            List<ExchangeRateResponse> exchangeRateDtos = new();

            foreach (var era in exchangeRatesActual)
            {
                exchangeRateDtos.Add(era.ToCurrencyDto());
            }

            return exchangeRateDtos;
        }
    }
}
