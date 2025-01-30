using CurrencyExchangeRates.Application.Mapper;
using CurrencyExchangeRates.Domain.Entities;

namespace CurrencyExchangeRates.UnitTests.Mapper
{
    public class DomainToDtoMapperTest
    {
        [Fact]
        public void ToCurrencyDto_ShouldReturnCorrectDto_WhenExchangeRatesIsValid()
        {
            // Arrange
            var exchangeRates = new ExchangeRate
            {
                Currency = new Currency { Code = "USD", Name = "Dollar" },
                Mid = 3.98m,
                BankName = "NBP",
                Date = DateOnly.Parse("2021-10-29")
            };

            // Act
            var result = exchangeRates.ToCurrencyDto();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("USD", result.Code);
            Assert.Equal("Dollar", result.Name);
            Assert.Equal(3.98m, result.Mid);
            Assert.Equal("NBP", result.BankName);
            Assert.Equal(DateOnly.Parse("2021-10-29"), result.Date);
        }
    }
}
