namespace CurrencyExchangeRates.Application.Model
{
    public record NbpTableA
    {
        public string Table { get; set; } = string.Empty;
        public string No { get; set; } = string.Empty;
        public DateOnly EffectiveDate { get; set; }
        public List<Rate> Rates { get; set; }
    }

    public record Rate
    {
        public string Currency { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Mid { get; set; }
    }
}
