using CurrencyExchangeRates.Application.Constants;
using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Domain.Entities;

namespace CurrencyExchangeRates.Application.Mapper
{
    public static class DtoToDomainMapper
    {
        public static List<ExchangeRate> ToExchangeRates(this NbpTableA nbpTableA, IEnumerable<Currency> currencies)
        {
            List<ExchangeRate> exchangeRates = new();

            nbpTableA.Rates.ForEach(n =>
            {
                Currency currency = currencies
                .FirstOrDefault(c => string.Equals(c.Code, n.Code, StringComparison.InvariantCultureIgnoreCase)) ?? new Currency { Code = n.Code, Name = n.Currency };

                ExchangeRate exchangeRate = new ExchangeRate { Mid = n.Mid, Date = nbpTableA.EffectiveDate, BankName = BankConstants.NBP };

                if (currency.Id == 0) exchangeRate.Currency = currency;
                else exchangeRate.CurrencyId = currency.Id;

                exchangeRates.Add(exchangeRate);
            });

            return exchangeRates;
        }
    }
}
