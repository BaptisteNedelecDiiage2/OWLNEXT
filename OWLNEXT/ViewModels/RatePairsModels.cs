using OWLNEXT.Business.Contract;
using OWLNEXT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OWLNEXT.ViewModels
{
    public class RatePairsModels
    {
        private IMoneyService _moneyRepository;
        private IRatePairsService _ratePairsRepository;
        public RatePairsModels(IMoneyService moneyService,IRatePairsService ratePairsService)
        {
            _moneyRepository = moneyService;
            _ratePairsRepository = ratePairsService;
        }
        public List<string> ListOfMoney { get => _moneyRepository.ListOfMoney().Result; }
        public List<RatePairs> RateMoney { get => _ratePairsRepository.RateMoney(ListOfMoney).Result; }
        public Dictionary<string,double[]> DictionaryRatePairsMinAndMaxDependingOnTheDate { get => _ratePairsRepository.GetRatePairsMinAndMaxDependingOnTheDate(ListOfMoney); }
    }
}
