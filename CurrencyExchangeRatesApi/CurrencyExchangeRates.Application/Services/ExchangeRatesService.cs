using CurrencyExchangeRates.Application.Mapper;
using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Infrastructure.Repositories;

namespace CurrencyExchangeRates.Application.Services
{
    public interface IExchangeRatesService
    {
        public Task<IEnumerable<ExchangeRateDto>> GetAllByLastDateAsync();
        public Task AddIfNoExistsAsync();
    }

    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly INbpClientService _nbpClientService;

        public ExchangeRatesService(IExchangeRateRepository exchangeRateRepository, INbpClientService nbpClientService, ICurrencyRepository currencyRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
            _nbpClientService = nbpClientService;
            _currencyRepository = currencyRepository;
        }

        public async Task<IEnumerable<ExchangeRateDto>> GetAllByLastDateAsync()
        {
            var exchangeRatesActual = await _exchangeRateRepository.GetLastOrderByDateAsync();

            List<ExchangeRateDto> exchangeRateDtos = new();

            foreach (var era in exchangeRatesActual)
            {
                exchangeRateDtos.Add(era.ToCurrencyDto());
            }

            return exchangeRateDtos;
        }

        public async Task AddIfNoExistsAsync()
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
