
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICalcTaxRateApp
    {
       Task<double> CalcTaxRate(double initValue, int months);
    }
}
