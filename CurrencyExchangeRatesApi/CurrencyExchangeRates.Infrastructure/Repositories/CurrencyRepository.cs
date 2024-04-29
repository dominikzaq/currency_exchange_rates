using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeRates.Infrastructure.Repositories
{
    public interface ICurrencyRepository
    {
        public Task<IEnumerable<Currency>> GetAllAsync();
    }

    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ExchangeRatesDbContext _dbContext;

        public CurrencyRepository(ExchangeRatesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _dbContext.Currency.AsNoTracking().ToListAsync();
        }
    }
}
