﻿namespace CurrencyExchangeRates.Domain.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public List<ExchangeRate> ExchangeRates { get; set; } = null!;
    }
}
