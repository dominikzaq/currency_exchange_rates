using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRates.Infrastructure.Repositories
{
    public interface IExchangeRateRepository
    {
        public Task AddRangeAsync(List<ExchangeRate> exchangeRates);
        public Task<IEnumerable<ExchangeRate>> GetLastOrderByDateAsync();
        public Task<DateOnly?> GetLastDateOrDefaultAsync();
    }

    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly ExchangeRatesDbContext _dbContext;

        public ExchangeRateRepository(ExchangeRatesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DateOnly?> GetLastDateOrDefaultAsync()
        {
            return await _dbContext.ExchangeRates.OrderByDescending(e => e.Date)
                .AsNoTracking()
                .Select(e => (DateOnly?)e.Date)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ExchangeRate>> GetLastOrderByDateAsync()
        {
            var lastDate = await GetLastDateOrDefaultAsync();

            if (lastDate.HasValue)
            {
                var test = _dbContext.ExchangeRates
                 .Include(e => e.Currency)
                 .Where(d => d.Date == lastDate)
                 .OrderBy(e => e.Currency.Name)
                 .ToQueryString();

                var exchangeRates = await _dbContext.ExchangeRates
                    .Include(e => e.Currency)
                    .Where(d => d.Date == lastDate)
                    .OrderBy(e => e.Currency.Name)
                    .AsNoTracking()
                    .ToListAsync();

                return exchangeRates;
            }

            return new List<ExchangeRate>();
        }

        public async Task AddRangeAsync(List<ExchangeRate> exchangeRates)
        {
            await _dbContext.ExchangeRates.AddRangeAsync(exchangeRates);
            await _dbContext.SaveChangesAsync();
        }
    }
}
