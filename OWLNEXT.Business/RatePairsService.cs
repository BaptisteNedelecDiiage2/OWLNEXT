using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OWLNEXT.Business.Contract;
using OWLNEXT.Entity;
using OWLNEXT.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OWLNEXT.Business
{
    public class RatePairsService : IRatePairsService
    {
        private readonly IRatePairsRepository _ratePairsmoneyRepository;
        private readonly HttpClient _httpClient;

        public RatePairsService(IRatePairsRepository ratePairsmoneyRepository, IHttpClientFactory factory)
        {
            _ratePairsmoneyRepository = ratePairsmoneyRepository;
            _httpClient = factory.CreateClient();
        }

        public Dictionary<string, double[]> GetRatePairsMinAndMaxDependingOnTheDate(List<string> moneypairs)
        {
            return _ratePairsmoneyRepository.GetRatePairsMinAndMaxDependingOnTheDate(moneypairs);
        }

        public Task<List<RatePairs>> RateMoney(List<string> moneypairs)
        {
            return _ratePairsmoneyRepository.GateMoneyRateAsync(moneypairs);
        }

        public async Task SaveMoneyRateAsync()
        {
            List<RatePairs> ratePairsSave = new List<RatePairs>();
            HttpResponseMessage response = await _httpClient.GetAsync("https://api.ibanfirst.com/PublicAPI/Cross");
            Cross objectCross = JsonConvert.DeserializeObject<Cross>(await response.Content.ReadAsStringAsync());
            List<string> moneypairs = objectCross.CrossList.Select(y => y.Instrument).ToList();

            foreach (var item in moneypairs)
            {
                HttpResponseMessage response2 = await _httpClient.GetAsync("https://api.ibanfirst.com/PublicAPI/Rate/" + item);
                RateMoney objectRateMoney = JsonConvert.DeserializeObject<RateMoney>(await response2.Content.ReadAsStringAsync());
                if (objectRateMoney != null)
                {
                    ratePairsSave.Add(objectRateMoney.Rate);
                }
            }

            await _ratePairsmoneyRepository.SaveMoneyRateAsync(ratePairsSave);
        }
    }
}
