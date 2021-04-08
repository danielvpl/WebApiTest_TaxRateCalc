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
                
        public async Task<double> CalcTaxRate(double initValue, int months)
        {
            try
            {
               var taxRate = await _service.GetExternalTaxRate();
               //taxRate = taxRate.Replace(".", ",");
               return _service.CalcTaxRate(initValue, months, double.Parse(taxRate));
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
