using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Application.Services;

namespace CurrencyExchangeRates.UnitTests.Services
{
    public class NbpClientServiceTest
    {
        [Fact]
        public async Task GetCurrentExchanageRatesTableAAsync_ShouldReturnExpectedResult()
        {
            var expected = new NbpTableA();
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.nbp.pl/api/exchangerates/tables/")
            };

            NbpClientService service = new NbpClientService(client);

            var result = await service.GetCurrentExchanageRatesTableAAsync();

            Assert.NotNull(result);
        }
    }
}
