using CurrencyExchangeRates.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRates.Infrastructure.Context
{
    public class ExchangeRatesDbContext : DbContext
    {
        public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();
        public DbSet<ExchangeRate> ExchangeRatesMid => Set<ExchangeRate>();
        public DbSet<Currency> Currency => Set<Currency>();

        public ExchangeRatesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasMany<ExchangeRate>(u => u.ExchangeRates)
                .WithOne(c => c.Currency)
                .HasForeignKey(c => c.CurrencyId);
            });
        }
    }
}
