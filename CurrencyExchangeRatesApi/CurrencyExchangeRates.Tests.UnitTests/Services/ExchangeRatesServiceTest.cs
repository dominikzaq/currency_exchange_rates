using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Application.Services;
using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Infrastructure.Repositories;
using Moq;

namespace CurrencyExchangeRates.Tests.UnitTests.Services
{
    public class ExchangeRatesServiceTest
    {
        private Mock<IExchangeRateRepository> _exchangeRatesRepositoryMock;
        private Mock<INbpClientService> _nbpClientServiceMock;
        private Mock<ICurrencyRepository> _currencyRepositoryMock;
        private IExchangeRatesService _exchangeRatesService;

        public ExchangeRatesServiceTest()
        {
            _exchangeRatesRepositoryMock = new Mock<IExchangeRateRepository>();
            _nbpClientServiceMock = new Mock<INbpClientService>();
            _currencyRepositoryMock = new Mock<ICurrencyRepository>();
            _exchangeRatesService = new ExchangeRatesService(_exchangeRatesRepositoryMock.Object, _nbpClientServiceMock.Object, _currencyRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllByLastDateAsync_ReturnsEmptyExchangeRateDtos()
        {
            // Arrange
            _exchangeRatesRepositoryMock.Setup(x => x.GetLastOrderByDateAsync()).ReturnsAsync(new List<ExchangeRate>());

            // Act

            var result = await _exchangeRatesService.GetAllByLastDateAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddIfNoExistsAsync_AddsExchangeRates_WhenNewRatesAvailable()
        {
            // Arrange
            var currencies = new List<Currency> { new Currency(), new Currency() };
            var nbpTableA = new NbpTableA
            {
                EffectiveDate = DateOnly.Parse("2024-04-29"),
                Rates = new List<Rate>
                {
                    new Rate {Code = "USD", Currency = "Dollar", Mid = 3.98m}
                }
            };

            _currencyRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(currencies);
            _exchangeRatesRepositoryMock.Setup(x => x.GetLastDateOrDefaultAsync()).ReturnsAsync((DateOnly?)null);
            _nbpClientServiceMock.Setup(x => x.GetCurrentExchanageRatesTableAAsync()).ReturnsAsync(nbpTableA);

            // Act
            await _exchangeRatesService.AddIfNoExistsAsync();

            // Assert
            _exchangeRatesRepositoryMock.Verify(x => x.AddRangeAsync(It.IsAny<List<ExchangeRate>>()), Times.Once);
        }
    }
}
