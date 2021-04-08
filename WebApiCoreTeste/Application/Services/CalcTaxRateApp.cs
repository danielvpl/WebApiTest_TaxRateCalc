using Application.Interfaces;
using Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class CalcTaxRateApp : ICalcTaxRateApp
    {
        private ICalcTaxRateService _service;

        public CalcTaxRateApp(ICalcTaxRateService service)
        {
            _service = service;
        }
                
        public double CalcTaxRate(double initValue, int months)
        {
            try
            {
               double taxRate = _service.GetTaxRate().Result;            
               return _service.CalcTaxRate(initValue, months, taxRate);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
