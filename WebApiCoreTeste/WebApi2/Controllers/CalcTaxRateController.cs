using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult<double>> CalculaJuros(double initValue, int months)
        {
            try
            {
                var data = await _calcTaxRateApp.CalcTaxRate(initValue, months);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<controller>/showmethecode
        [HttpGet("showmethecode")]
        public ActionResult<string> ShowMeTheCode()
        {
            return Ok(@"https://github.com/danielvpl/WebApiTest_TaxRateCalc.git");
        }
    }
}
