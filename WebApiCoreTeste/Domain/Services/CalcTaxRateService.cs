using Domain.Interfaces.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CalcTaxRateService : ICalcTaxRateService
    {
        public double CalcTaxRate(double initValue, int months, double taxRate) 
            => this.Truncate(initValue * Math.Pow((1 + taxRate), months));

        public async Task<string> GetExternalTaxRate()
        {
            try
            {
                //Info: bypass ssl certificate check
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                var client = new HttpClient(clientHandler);
                client.BaseAddress = new System.Uri("http://localhost:8550/webapitest1/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/TaxRate/taxajuros");
                if (response.IsSuccessStatusCode)
                {   
                    //GET
                    client.Dispose();
                    return await response.Content.ReadAsStringAsync();                        
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("Impossível acessar a Api1. Detalhes: " + ex.Message);
            }
            return "0";
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
