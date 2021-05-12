using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICalcTaxRateService
    {        
        double CalcTaxRate(double initValue, int months, double taxRate);
    }
}
