using FluentAssertions;
using TesteB3.Domain.ResponseModels;

namespace TesteB3.Tests.Domain.ResponseModels
{
    public class CdbResponseModelTest
    {
        [Trait("Constructor", "Success test scenario.")]
        [Theory]
        [InlineData(10.123456223, 8.987654223, 10.12, 8.98)]
        [InlineData(33.753456223, 28.987654223, 33.75, 28.98)]
        [InlineData(48.883456223, 48.987654223, 48.88, 48.98)]
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
        [InlineData(48.889456223, 48.987654223, 48.89, 48.99)]
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
