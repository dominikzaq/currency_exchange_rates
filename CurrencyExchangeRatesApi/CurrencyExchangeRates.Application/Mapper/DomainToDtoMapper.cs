using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Domain.Entities;

namespace CurrencyExchangeRates.Application.Mapper
{
    public static class DomainToDtoMapper
    {
        public static ExchangeRateDto ToCurrencyDto(this ExchangeRate exchangeRates)
        {
            return new ExchangeRateDto
            {
                Code = exchangeRates.Currency.Code,
                Name = exchangeRates.Currency.Name,
                Mid = exchangeRates.Mid,
                BankName = exchangeRates.BankName,
                Date = exchangeRates.Date,
            };
        }
    }
}
