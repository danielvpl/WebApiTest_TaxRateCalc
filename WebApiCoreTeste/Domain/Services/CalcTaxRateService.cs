using Domain.Interfaces.Services;
using System;

namespace Domain.Services
{
    public class CalcTaxRateService : ICalcTaxRateService
    {
        public double CalcTaxRate(double initValue, int months, double taxRate) 
            => this.Truncate(initValue * Math.Pow((1 + taxRate), months));

        private double Truncate(double valor)
        {
            valor *= 100;
            valor = Math.Truncate(valor);
            valor /= 100;
            return valor;
        }
    }
}
