namespace CurrencyExchangeRates.Domain.Entities
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public DateOnly Date { get; set; }
        public string BankName { get; set; } = string.Empty;
        public decimal Mid { get; set; }
        public Currency Currency { get; set; } = null!;
    }
}
