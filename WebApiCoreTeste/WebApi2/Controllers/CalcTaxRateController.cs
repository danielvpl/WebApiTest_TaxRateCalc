using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class CalcTaxRateController : ControllerBase
    {
        private ICalcTaxRateApp _calcTaxRateApp;

        public CalcTaxRateController(ICalcTaxRateApp calcTaxTRateApp)
        {
            _calcTaxRateApp = calcTaxTRateApp;
        }

        // GET: api/<controller>
        [HttpGet("calculajuros")]       
        public ActionResult<double> CalculaJuros(double initValue, int months)
        {
            try
            {
                var data = _calcTaxRateApp.CalcTaxRate(initValue, months);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
