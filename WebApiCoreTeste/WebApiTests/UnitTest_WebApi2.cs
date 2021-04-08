using Application.Interfaces;
using Application.Repositories;
using Domain.Interfaces.Services;
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
        public async void IsOkAndReturnsCalcResult()
        {
            //Given
            double initValue = 100;
            int months = 5;
            string tax = "0.01";
            double result = 105.01;

            _mockCalcTaxRateService
                .Setup(x => x.GetExternalTaxRate())
                .ReturnsAsync(tax);

            _mockCalcTaxRateService
                .Setup(x => x.CalcTaxRate(initValue, months, double.Parse(tax)))
                .Returns(result);

            // Act
            var data = await _controller.CalculaJuros(initValue, months);

            // Assert
            Assert.IsType<OkObjectResult>(data.Result);            
            Assert.IsAssignableFrom<ActionResult<double>>(data);
            Assert.Equal(result, ((ObjectResult)data.Result).Value);
        }

        [Fact]
        public void ReturnsProjectRepositoryUrl()
        {
            //Given
            string repositoryUrl = "https://github.com/danielvpl/WebApiTest_TaxRateCalc.git";
            
            // Act
            var data =  _controller.ShowMeTheCode();

            // Assert
            Assert.IsType<OkObjectResult>(data.Result);
            Assert.IsAssignableFrom<ActionResult<string>>(data);
            Assert.Equal(repositoryUrl, ((ObjectResult)data.Result).Value);
        }
    }
}
