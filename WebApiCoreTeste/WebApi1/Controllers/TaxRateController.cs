using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class TaxRateController : ControllerBase
    {
        private ITaxRateApp _taxRateApp;

        public TaxRateController(ITaxRateApp taxTRateApp)
        {
            _taxRateApp = taxTRateApp;
        }

        // GET: api/<controller>
        [HttpGet("taxaJuros")]       
        public ActionResult<double> Get()
        {
            try
            {
                var data = _taxRateApp.Get();
                return Ok(data);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
