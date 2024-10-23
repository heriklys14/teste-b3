using FluentAssertions;
using TesteB3.Domain.ResponseModels;

namespace TesteB3.Tests.Domain.ResponseModels
{
    public class CdbResponseModelTest
    {
        [Trait("Constructor", "Success test scenario.")]
        [Theory]
        [InlineData(10.123456223, 8.987254223, 10.12, 8.99)]
        [InlineData(11.123456223, 10.984254223, 11.12, 10.98)]
        [InlineData(33.753456223, 28.988654823, 33.75, 28.99)]
        [InlineData(58.883456223, 48.987004923, 58.88, 48.99)]
        public void CdbResponseModel_Success(double grossValue, double netValue, double roundedGrossValue, double roundedNetValue)
        {
            //Arrange & Act
            var model = new CdbResponseModel(grossValue, netValue);

            //Assert
            model.GrossValue.Should().Be(roundedGrossValue);
            model.NetValue.Should().Be(roundedNetValue);
        }

        [Trait("Constructor", "Fail test scenario.")]
        [Theory]
        [InlineData(10.123456223, 8.987654223, 10.123456223, 8.987654223)]
        [InlineData(33.753456223, 28.987654223, 33.7534, 28.987)]
        [InlineData(48.883456223, 48.987654223, 48.8, 48.9)]
        [InlineData(48.889456223, 48.987654223, 48.88, 48.98)]
        [InlineData(55.884456223, 52.013554203, 55.89, 52.02)]
        public void CdbResponseModel_Fail(double grossValue, double netValue, double roundedGrossValue, double roundedNetValue)
        {
            //Arrange & Act
            var model = new CdbResponseModel(grossValue, netValue);

            //Assert
            model.GrossValue.Should().NotBe(roundedGrossValue);
            model.NetValue.Should().NotBe(roundedNetValue);
        }
    }
}
