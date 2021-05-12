using Application.Interfaces;
using Application.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using WebApiCore.Controllers;
using Xunit;

namespace WebApiTests
{
    public class UnitTest_WebApi2
    {
        private readonly CalcTaxRateController _controller;
        private readonly Mock<ICalcTaxRateApp> _mockCalcTaxRateApp;
        
        public UnitTest_WebApi2()
        {
            _mockCalcTaxRateApp = new Mock<ICalcTaxRateApp>();
            _controller = new CalcTaxRateController(_mockCalcTaxRateApp.Object);
        }

        [Fact]
        public async void IsOkAndReturnsCalcResult()
        {
            //Given
            double initValue = 100;
            int months = 5;
            string tax = "0.01";
            double result = 105.01;

            _mockCalcTaxRateApp
                .Setup(x => x.GetExternalTaxRate())
                .ReturnsAsync(tax);

            _mockCalcTaxRateApp
                .Setup(x => x.CalcTaxRate(initValue, months))
                .ReturnsAsync(result);

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
