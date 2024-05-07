using CurrencyExchangeRates.Application.Commands;
using CurrencyExchangeRates.Application.Mapper;
using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Application.Services;
using CurrencyExchangeRates.Infrastructure.Repositories;
using MediatR;

namespace CurrencyExchangeRates.Application.Handlers
{
    public class UpdateExchangeRateHandler : IRequestHandler<UpdateExchangeRateCommand>
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly INbpClientService _nbpClientService;

        public UpdateExchangeRateHandler(IExchangeRateRepository exchangeRateRepository, ICurrencyRepository currencyRepository, INbpClientService nbpClientService)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _currencyRepository = currencyRepository;
            _nbpClientService = nbpClientService;
        }

        public async Task Handle(UpdateExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var currencies = await _currencyRepository.GetAllAsync();
            var lastDate = await _exchangeRateRepository.GetLastDateOrDefaultAsync();

            NbpTableA? nbpTableA = await _nbpClientService.GetCurrentExchanageRatesTableAAsync();

            if (nbpTableA?.EffectiveDate.CompareTo(lastDate) != 0)
            {
                var exchangeRates = nbpTableA!.ToExchangeRates(currencies);
                await _exchangeRateRepository.AddRangeAsync(exchangeRates);
            }
        }
    }
}
