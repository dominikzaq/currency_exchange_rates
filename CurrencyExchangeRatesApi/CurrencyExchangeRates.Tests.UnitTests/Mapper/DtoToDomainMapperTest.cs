using CurrencyExchangeRates.Application.Mapper;
using CurrencyExchangeRates.Application.Model;
using CurrencyExchangeRates.Domain.Entities;

namespace CurrencyExchangeRates.Tests.UnitTests.Mapper
{
    public class DtoToDomainMapperTest
    {
        [Fact]
        public void ToExchangeRates_ShouldReturnCorrectList_WhenNbpTableAIsValid()
        {
            // Arrange
            var nbpTableA = new NbpTableA
            {
                EffectiveDate = DateOnly.Parse("2021-10-29"),
                Rates = new List<Rate>
                {
                    new Rate { Code = "USD", Currency = "Dollar", Mid = 3.98m },
                    new Rate { Code = "EUR", Currency = "Euro", Mid = 4.50m }
                }
            };

            var currencies = new List<Currency>
            {
            };

            // Act
            var result = nbpTableA.ToExchangeRates(currencies);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("USD", result[0].Currency.Code);
            Assert.Equal("Dollar", result[0].Currency.Name);
            Assert.Equal(3.98m, result[0].Mid);
            Assert.Equal("Nbp", result[0].BankName);
            Assert.Equal(DateOnly.Parse("2021-10-29"), result[0].Date);
        }
    }
}
