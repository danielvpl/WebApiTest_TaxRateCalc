using Application.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class CalcTaxRateApp : ICalcTaxRateApp
    {
        private ICalcTaxRateService _service;
        private IConfiguration _configuration;

        public CalcTaxRateApp(ICalcTaxRateService service, IConfiguration configuration)
        {
            _configuration = configuration;
            _service = service;
        }
                
        public async Task<double> CalcTaxRate(double initValue, int months)
        {
            try
            {
               var taxRate = await GetExternalTaxRate();
               return _service.CalcTaxRate(initValue, months, double.Parse(taxRate));
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<string> GetExternalTaxRate()
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                var client = new HttpClient(clientHandler);
                client.BaseAddress = new Uri(_configuration.GetValue<string>("ExternalApiUrl"));
                client.DefaultRequestHeaders.Accept.Clear();
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
    }
}
