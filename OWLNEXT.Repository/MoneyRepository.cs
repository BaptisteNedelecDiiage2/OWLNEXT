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
        private readonly string _urlApi = "https://api.ibanfirst.com/PublicAPI/Cross";
        public async Task<List<string>> ListOfMoney()
        {
            using (HttpClient client = new HttpClient(new HttpClientHandler()))
            {
                HttpResponseMessage response = await client.GetAsync(_urlApi);
                Cross objectCross =  JsonConvert.DeserializeObject<Cross>(await response.Content.ReadAsStringAsync());
                return objectCross.CrossList.Select(y => y.Instrument).ToList();
            }
        }
    }
}
