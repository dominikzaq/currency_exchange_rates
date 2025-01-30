namespace CurrencyExchangeRates.Application.Model
{
    public record NbpTableA
    {
        public string Table { get; init; } = string.Empty;
        public string No { get; init; } = string.Empty;
        public DateOnly EffectiveDate { get; init; }
        public List<Rate> Rates { get; init; }
    }

    public record Rate
    {
        public string Currency { get; init; } = string.Empty;
        public string Code { get; init; } = string.Empty;
        public decimal Mid { get; init; }
    }
}
