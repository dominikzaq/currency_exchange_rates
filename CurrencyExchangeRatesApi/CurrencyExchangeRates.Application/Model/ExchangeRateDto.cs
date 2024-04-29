namespace CurrencyExchangeRates.Application.Model
{
    public record ExchangeRateDto
    {
        public string BankName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Mid { get; set; }
        public DateOnly Date { get; set; }
    }
}
