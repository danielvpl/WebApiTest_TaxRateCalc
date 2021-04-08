using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class TaxRateService : ITaxRateService
    {
        public double Get() => 0.01;
    }
}
