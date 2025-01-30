using CurrencyExchangeRates.Application.Responses;
using CurrencyExchangeRates.Domain.Entities;

namespace CurrencyExchangeRates.Application.Mapper
{
    public static class DomainToDtoMapper
    {
        public static ExchangeRateResponse ToCurrencyDto(this ExchangeRate exchangeRates)
        {
            return new ExchangeRateResponse
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
