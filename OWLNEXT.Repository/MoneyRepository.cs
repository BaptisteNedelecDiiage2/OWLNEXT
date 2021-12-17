using Newtonsoft.Json;
using OWLNEXT.Entity;
using OWLNEXT.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Repository
{
    public class MoneyRepository : IMoneyRepository
    {
        private const string _urlApi = "https://api.ibanfirst.com/PublicAPI/Cross";
        private readonly HttpClient _httpClient;

        public MoneyRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<string>> ListOfMoney()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_urlApi);
            Cross objectCross = JsonConvert.DeserializeObject<Cross>(await response.Content.ReadAsStringAsync());
            return objectCross.CrossList.Select(y => y.Instrument).ToList();

        }
    }
}
