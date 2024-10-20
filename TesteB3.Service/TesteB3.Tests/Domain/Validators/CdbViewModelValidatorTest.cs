using FluentAssertions;
using TesteB3.Domain.Validators;
using TesteB3.Domain.ViewModels;

namespace TesteB3.Tests.Domain.Validators
{
    public class CdbViewModelValidatorTest
    {
        private readonly CdbViewModelValidator validator;

        public CdbViewModelValidatorTest()
        {
            validator = new CdbViewModelValidator();
        }

        [Trait("Validate", "Success test scenario.")]
        [Theory]
        [InlineData(0.01, 2)]
        [InlineData(345, 26)]
        [InlineData(1000.45, 14)]
        public void CdbViewModelValidator_Success(double value, int interval)
        {
            //Arrange
            var model = new CdbViewModel { Value = value, Interval = interval };

            //Act
            var result = validator.Validate(model);

            //Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Trait("Validate", "Fail test scenario.")]
        [Theory]
        [MemberData(nameof(GetData_CdbViewModelValidator_Fail))]
        public void CdbViewModelValidator_Fail(double value, int interval, List<string> errorMessages)
        {
            //Arrange
            var model = new CdbViewModel { Value = value, Interval = interval };

            //Act
            var result = validator.Validate(model);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().AllSatisfy(error => errorMessages.Contains(error.ErrorMessage));
        }

        public static TheoryData<double, int, List<string>> GetData_CdbViewModelValidator_Fail()
        {
            return new TheoryData<double, int, List<string>>
            {
                { 0, 3, [ "O valor informado deve ser maior que R$ 0,01." ] },
                { 1.0, 1, [ "O intervalo informado deve ser maior que 1 (um)." ] },
                { 0, 0, [ "O valor informado deve ser maior que R$ 0,01.", "O intervalo informado deve ser maior que 1 (um)." ] }
            };
        }
    }
}
