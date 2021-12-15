using OWLNEXT.Business.Contract;
using OWLNEXT.Entity;
using OWLNEXT.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Business
{
    public class RatePairsService : IRatePairsService
    {
        private readonly IRatePairsRepository _ratePairsmoneyRepository;
        public RatePairsService( IRatePairsRepository ratePairsmoneyRepository)
        {
            _ratePairsmoneyRepository = ratePairsmoneyRepository;
        }

        public Dictionary<string, double[]> GetRatePairsMinAndMaxDependingOnTheDate(List<string> moneypairs)
        {
            return _ratePairsmoneyRepository.GetRatePairsMinAndMaxDependingOnTheDate(moneypairs);
        }

        public Task<List<RatePairs>> RateMoney(List<string> moneypairs)
        {
            return _ratePairsmoneyRepository.RateMoney(moneypairs);
        }
    }
}
