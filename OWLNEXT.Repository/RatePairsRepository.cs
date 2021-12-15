using Newtonsoft.Json;
using OWLNEXT.Entity;
using OWLNEXT.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OWLNEXT.Repository
{
    public class RatePairsRepository : IRatePairsRepository
    {
        private readonly string _urlApi = "https://api.ibanfirst.com/PublicAPI/Rate/";
        private readonly string fileName = @"E:\Diiage\CSharpBDDJson.txt";
        public async Task<List<RatePairs>> RateMoney(List<string> moneypairs)
        {

            using (HttpClient client = new HttpClient(new HttpClientHandler()))
            {
                List<RatePairs> ratePairs = new List<RatePairs>();
                List<RatePairs> ratePairsSave = new List<RatePairs>();
                foreach (var item in moneypairs)
                {
                    HttpResponseMessage response = await client.GetAsync(_urlApi + item);
                    RateMoney objectRateMoney = JsonConvert.DeserializeObject<RateMoney>(await response.Content.ReadAsStringAsync());
                    ratePairs.Add(objectRateMoney.Rate);
                    ratePairsSave.Add(objectRateMoney.Rate);
                }
                using (StreamReader file = File.OpenText(fileName))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    List<RatePairs> listRatePairs = (List<RatePairs>)serializer.Deserialize(file, typeof(List<RatePairs>));
                    foreach (var item in listRatePairs)
                    {
                        ratePairsSave.Add(item);
                    }
                }
                string json = System.Text.Json.JsonSerializer.Serialize(ratePairsSave);
                File.WriteAllText(fileName, json);
                return ratePairs;

            }
        }
        public Dictionary<string, double[]> GetRatePairsMinAndMaxDependingOnTheDate(List<string> moneypairs)
        {
            Dictionary<string, List<RatePairs>> dictionaryRatePairs = new Dictionary<string, List<RatePairs>>();
            Dictionary<string, double[]> dictionaryRatePairsMinAndMaxDependingOnTheDate = new Dictionary<string, double[]>();
            using (StreamReader file = File.OpenText(fileName))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                List<RatePairs> listRatePairs = (List<RatePairs>)serializer.Deserialize(file, typeof(List<RatePairs>));
                foreach (var item in moneypairs)
                {
                    dictionaryRatePairs.Add(item, listRatePairs.Where(x=>x.Instrument == item && x.Date.ToString("d") == DateTime.Today.ToString("d")).ToList());
                }
                foreach (var item in dictionaryRatePairs)
                {
                    double minRateTOday = item.Value.Min(p => p.Rate);
                    double maxRateTOday = item.Value.Max(p => p.Rate);
                    double[] RateTOday = { minRateTOday, maxRateTOday };
                    dictionaryRatePairsMinAndMaxDependingOnTheDate.Add(item.Key, RateTOday);
                }
            }
            return dictionaryRatePairsMinAndMaxDependingOnTheDate;

        }
    }
}
