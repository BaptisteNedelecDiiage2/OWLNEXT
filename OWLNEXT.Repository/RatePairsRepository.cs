using Newtonsoft.Json;
using OWLNEXT.Entity;
using OWLNEXT.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OWLNEXT.Repository
{
    public class RatePairsRepository : IRatePairsRepository
    {
        private readonly string _urlApi = "https://api.ibanfirst.com/PublicAPI/Rate/";
        private readonly string fileName = @"E:\Diiage\CSharpBDDJson.txt";
        private readonly HttpClient _httpClient;

        public RatePairsRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<RatePairs>> GateMoneyRateAsync(List<string> moneypairs)
        {
            List<RatePairs> ratePairs = new List<RatePairs>();

            foreach (var item in moneypairs)
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_urlApi + item);
                RateMoney objectRateMoney = JsonConvert.DeserializeObject<RateMoney>(await response.Content.ReadAsStringAsync());
                if (objectRateMoney != null)
                {
                    ratePairs.Add(objectRateMoney.Rate);
                }
            }

            return ratePairs;
        }

        public async Task SaveMoneyRateAsync(List<RatePairs> ratePairsSave)
        {
            using (StreamReader file = File.OpenText(fileName))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                List<RatePairs> listRatePairs = (List<RatePairs>)serializer.Deserialize(file, typeof(List<RatePairs>));
                ratePairsSave.AddRange(listRatePairs);
            }
            string json = System.Text.Json.JsonSerializer.Serialize(ratePairsSave);
            await File.WriteAllTextAsync(fileName, json);
        }


        public Dictionary<string, double[]> GetRatePairsMinAndMaxDependingOnTheDate(List<string> moneypairs)
        {
            Dictionary<string, double[]> dictionaryRatePairsMinAndMaxDependingOnTheDate = new Dictionary<string, double[]>();
            using (StreamReader file = File.OpenText(fileName))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                List<RatePairs> listRatePairs = (List<RatePairs>)serializer.Deserialize(file, typeof(List<RatePairs>));
                Dictionary<string, List<RatePairs>> dictionaryRatePairs = moneypairs.ToDictionary(item => item, item =>
                    listRatePairs.Where(x => x.Instrument == item && x.Date.ToString("d") == DateTime.Today.ToString("d")).ToList()
                        );
                foreach (var (key, value) in dictionaryRatePairs)
                {
                    double minRateTOday = value.Min(p => p.Rate);
                    double maxRateTOday = value.Max(p => p.Rate);
                    double[] RateTOday = { minRateTOday, maxRateTOday };
                    dictionaryRatePairsMinAndMaxDependingOnTheDate.Add(key, RateTOday);
                }
            }
            return dictionaryRatePairsMinAndMaxDependingOnTheDate;

        }
    }
}
