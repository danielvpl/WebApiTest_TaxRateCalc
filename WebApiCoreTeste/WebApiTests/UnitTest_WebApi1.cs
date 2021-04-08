using Application.Interfaces;
using Application.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Controllers;
using Xunit;

namespace WebApiTests
{
    public class UnitTest
    {
        private readonly TaxRateController _controller;
        private readonly ITaxRateApp _taxRateApp;
        private readonly ITaxRateService _service;

        public UnitTest()
        {
            _service = new TaxRateService();
            _taxRateApp = new TaxRateApp(_service);
            _controller = new TaxRateController(_taxRateApp);
        }

        [Fact]
        public void IsOkObjectResult()
        {
            // Act
            var data = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(data.Result);
        }

        [Fact]
        public void ShouldReturnTax()
        {
            // Act
            var data = _controller.Get();

            // Assert            
            Assert.IsAssignableFrom<ActionResult<double>>(data);
            Assert.Equal(decimal.Parse("0,01"), ((ObjectResult)data.Result).Value);
        }
    }
}
