using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using TesteB3.Api.Controllers;
using TesteB3.Domain.Interfaces;
using TesteB3.Domain.ResponseModels;
using TesteB3.Domain.ViewModels;

namespace TesteB3.Tests.Api.Controllers
{
    public class CalculatorControllerTest
    {
        private readonly Mock<ICdbService> cdbServiceMock;

        private readonly CalculatorController controller;

        public CalculatorControllerTest()
        {
            cdbServiceMock = new Mock<ICdbService>();

            controller = new CalculatorController(cdbServiceMock.Object);
        }

        [Trait("Constructor", "Success test scenario.")]
        [Fact]
        public void ComputeCdb_Success()
        {
            //Arrange
            var model = new CdbViewModel();
            var responseModel = new CdbResponseModel(10.52, 8.52);

            cdbServiceMock.Setup(m => m.ComputeCdbValue(model)).Returns(responseModel);

            //Act
            var result = controller.ComputeCdb(model);
            var response = Assert.IsType<OkObjectResult>(result);

            //Assert
            response.StatusCode.Should().Be((int)HttpStatusCode.OK);
            response.Value.Should().BeEquivalentTo(responseModel);
        }

        [Trait("Constructor", "Fail test scenario.")]
        [Theory]
        [MemberData(nameof(GetData_ComputeCdb_Fail))]
        public void ComputeCdb_Fail(string[] messages)
        {
            //Arrange
            var model = new CdbViewModel();
            var exception = new InvalidOperationException(string.Join(';', messages));

            cdbServiceMock.Setup(m => m.ComputeCdbValue(model)).Throws(exception);

            //Act
            var result = controller.ComputeCdb(model);
            var response = Assert.IsType<BadRequestObjectResult>(result);

            //Assert
            response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            response.Value.Should().BeEquivalentTo(new { messages });
        }

        public static TheoryData<string[]> GetData_ComputeCdb_Fail()
        {
            return
            [
                [ "Falha ao executar processo" ],
                [ "Falha durante cálculo", "Valores inválidos para processamento" ]
            ];
        }
    }
}
