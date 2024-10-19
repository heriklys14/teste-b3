using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using TesteB3.Domain.ResponseModels;
using TesteB3.Domain.Services;
using TesteB3.Domain.ViewModels;

namespace TesteB3.Tests.Domain.Services
{
    public class CdbServiceTest
    {
        private readonly Mock<IValidator<CdbViewModel>> modelValidatorMock;
        private readonly CdbService service;

        public CdbServiceTest()
        {
            modelValidatorMock = new Mock<IValidator<CdbViewModel>>();

            service = new CdbService(modelValidatorMock.Object);
        }


        [Trait("ComputeCdbValue", "Success test scenario.")]
        [InlineData(10.0, 3, 10.294443535300477, 7.97819373985787)]
        [InlineData(20.0, 9, 21.819189956487996, 17.4553519651904)]
        [InlineData(30.0, 18, 35.705778776798027, 29.457267490858371)]
        [InlineData(40.0, 30, 53.467361108018714, 45.447256941815908)]
        [Theory]
        public void ComputeCdbValue_Success(double value, int interval, double grossValue, double netvalue)
        {
            //Arrange
            var viewModel = new CdbViewModel { Value = value, Interval = interval };
            var expectedResult = new CdbResponseModel(grossValue, netvalue);
            var validationResult = new ValidationResult();

            modelValidatorMock.Setup(m => m.Validate(It.Is<CdbViewModel>(x => x.Value == viewModel.Value && x.Interval == viewModel.Interval))).Returns(validationResult);

            //Act
            var result = service.ComputeCdbValue(viewModel);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
            modelValidatorMock.Verify(m => m.Validate(It.Is<CdbViewModel>(x => x.Value == viewModel.Value && x.Interval == viewModel.Interval)), Times.Once);
        }

        [Trait("ComputeCdbValue", "Fail test scenario.")]
        [MemberData(nameof(GetData_ComputeCdbValue_Fail))]
        [Theory]
        public void ComputeCdbValue_Fail(List<ValidationFailure> validationFailures)
        {
            //Arrange
            var viewModel = new CdbViewModel { Value = 10.0, Interval = 3 };
            var validationResult = new ValidationResult(validationFailures);

            modelValidatorMock.Setup(m => m.Validate(It.Is<CdbViewModel>(x => x.Value == viewModel.Value && x.Interval == viewModel.Interval))).Returns(validationResult);

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => service.ComputeCdbValue(viewModel));
            var messages = exception.Message.Split(';');

            //Assert
            messages.Should().Contain(validationFailures.Select(x => x.ErrorMessage));
            modelValidatorMock.Verify(m => m.Validate(It.Is<CdbViewModel>(x => x.Value == viewModel.Value && x.Interval == viewModel.Interval)), Times.Once);
        }

        public static TheoryData<List<ValidationFailure>> GetData_ComputeCdbValue_Fail()
        {
            return
            [
                [ new ValidationFailure("value", "Valor inválido") ]
            ];
        }
    }
}
