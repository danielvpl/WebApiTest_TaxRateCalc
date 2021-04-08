using Application.Interfaces;
using Application.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiCore.Controllers;
using Xunit;

namespace WebApiTests
{
    public class UnitTest_WebApi2
    {
        private readonly CalcTaxRateController _controller;
        private readonly ICalcTaxRateApp _calcTaxRateApp;
        private readonly Mock<ICalcTaxRateService> _mockCalcTaxRateService;

        public UnitTest_WebApi2()
        {
            _mockCalcTaxRateService = new Mock<ICalcTaxRateService>();
            _calcTaxRateApp = new CalcTaxRateApp(_mockCalcTaxRateService.Object);
            _controller = new CalcTaxRateController(_calcTaxRateApp);
        }

        [Fact]
        public void IsOkAndReturnsCalcResult()
        {
            //Given
            double initValue = 100;
            int months = 5;
            double tax = 0.01;

            _mockCalcTaxRateService
                .Setup(x => x.GetTaxRate())
                .ReturnsAsync(tax);

            _mockCalcTaxRateService
                .Setup(x => x.CalcTaxRate(initValue, months, tax))
                .Returns(105.10);

            // Act
            var data = _controller.CalculaJuros(initValue, months);

            // Assert
            Assert.IsType<OkObjectResult>(data.Result);            
            Assert.IsAssignableFrom<ActionResult<double>>(data);
            Assert.Equal(105.10, ((ObjectResult)data.Result).Value);
        }
    }
}
