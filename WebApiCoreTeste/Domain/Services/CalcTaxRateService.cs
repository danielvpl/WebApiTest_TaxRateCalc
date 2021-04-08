using Domain.Interfaces.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CalcTaxRateService : ICalcTaxRateService
    {
        public double CalcTaxRate(double initValue, int months, double taxRate) => this.Truncate(initValue * Math.Pow((1 + taxRate), (double)months));

        public async Task<double> GetTaxRate()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri("http://localhost:20550");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/TaxRate/taxaJuros");
                    if (response.IsSuccessStatusCode)
                    {  //GET
                        var taxRate = await response.Content.ReadAsStringAsync();
                        return double.Parse(taxRate.Replace(".", ","));
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Impossível acessar a Api1, o projeto WebApi1 deve ser iniciado em paralelo ao WebApi2.");
            }
            return 0;
        }

        private double Truncate(double valor)
        {
            valor *= 100;
            valor = Math.Truncate(valor);
            valor /= 100;
            return valor;
        }
    }
}
