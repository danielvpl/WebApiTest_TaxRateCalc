using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICalcTaxRateService
    {
        Task<double> GetTaxRate();
        double CalcTaxRate(double initValue, int months, double taxRate);
    }
}
