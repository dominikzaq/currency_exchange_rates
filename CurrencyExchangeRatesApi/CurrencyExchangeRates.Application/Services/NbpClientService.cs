using CurrencyExchangeRates.Application.Model;
using System.Net.Http.Json;

namespace CurrencyExchangeRates.Application.Services
{
    public interface INbpClientService
    {
        public Task<NbpTableA?> GetCurrentExchanageRatesTableAAsync();
    }

    public class NbpClientService : INbpClientService
    {
        private readonly HttpClient _client;

        public NbpClientService(HttpClient client)
        {
            _client = client;
        }


        public async Task<NbpTableA?> GetCurrentExchanageRatesTableAAsync()
        {
            var list = await _client.GetFromJsonAsync<List<NbpTableA>>("a/?format=json");

            return list?.FirstOrDefault();
        }
    }
}
