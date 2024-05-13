using CurrencyExchangeRates.Application.Commands;
using CurrencyExchangeRates.Application.Handlers;
using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Application.Queries;
using CurrencyExchangeRates.Application.Services;
using CurrencyExchangeRates.Domain.Entities;
using CurrencyExchangeRates.Infrastructure.Repositories;
using Moq;

namespace CurrencyExchangeRates.UnitTests.Services
{
    public class ExchangeRatesServiceTest
    {
        private Mock<IExchangeRateRepository> _exchangeRatesRepositoryMock;
        private Mock<INbpClientService> _nbpClientServiceMock;
        private Mock<ICurrencyRepository> _currencyRepositoryMock;

        public ExchangeRatesServiceTest()
        {
            _exchangeRatesRepositoryMock = new Mock<IExchangeRateRepository>();
            _nbpClientServiceMock = new Mock<INbpClientService>();
            _currencyRepositoryMock = new Mock<ICurrencyRepository>();
        }

        [Fact]
        public async Task GetAllByLastDateAsync_ReturnsEmptyExchangeRateDtos()
        {
            // Arrange
            _exchangeRatesRepositoryMock.Setup(x => x.GetLastOrderByDateAsync()).ReturnsAsync(new List<ExchangeRate>());
            var handler = new GetAllExchangeRatesByLastDateHandler(_exchangeRatesRepositoryMock.Object);

            // Act
            var result = await handler.Handle(new GetAllExchangeRatesByLastDateQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddIfNoExistsAsync_AddsExchangeRates_WhenNewRatesAvailable()
        {
            // arrange
            var currencies = new List<Currency> { new Currency(), new Currency() };
            var nbptablea = new NbpTableA
            {
                EffectiveDate = DateOnly.Parse("2024-04-29"),
                Rates = new List<Rate>
                {
                    new Rate { Code = "usd", Currency = "dollar", Mid = 3.98m}
                }
            };

            _currencyRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(currencies);
            _exchangeRatesRepositoryMock.Setup(x => x.GetLastDateOrDefaultAsync()).ReturnsAsync((DateOnly?)null);
            _nbpClientServiceMock.Setup(x => x.GetCurrentExchanageRatesTableAAsync()).ReturnsAsync(nbptablea);
            var handler = new UpdateExchangeRateHandler(_exchangeRatesRepositoryMock.Object, _currencyRepositoryMock.Object, _nbpClientServiceMock.Object);

            // act
            var result = await handler.Handle(new UpdateExchangeRateCommand(), CancellationToken.None);

            // assert
            _exchangeRatesRepositoryMock.Verify(x => x.AddRangeAsync(It.IsAny<List<ExchangeRate>>()), Times.Once);
            Assert.True(result);
        }
    }
}
