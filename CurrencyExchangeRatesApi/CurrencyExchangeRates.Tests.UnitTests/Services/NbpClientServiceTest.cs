using CurrencyExchangeRates.Application.Services;

namespace CurrencyExchangeRates.Tests.UnitTests.Services
{
    public class NbpClientServiceTest
    {
        [Fact]
        public async Task GetCurrentExchanageRatesTableAAsync()
        {
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
