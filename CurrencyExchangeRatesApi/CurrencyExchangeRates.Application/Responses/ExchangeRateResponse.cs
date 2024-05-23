namespace CurrencyExchangeRates.Application.Responses
{
    public record ExchangeRateResponse
    {
        public string BankName { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string Code { get; init; } = string.Empty;
        public decimal Mid { get; init; }
        public DateOnly Date { get; init; }
    }
}
