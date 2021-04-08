using Application.Interfaces;
using Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class TaxRateApp : ITaxRateApp
    {
        private ITaxRateService _service;

        public TaxRateApp(ITaxRateService service)
        {
            _service = service;
        }
                
        public double Get()
        {
            return _service.Get();
        }
    }
}
