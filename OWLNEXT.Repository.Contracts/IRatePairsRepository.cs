using OWLNEXT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Repository.Contracts
{
    public interface IRatePairsRepository
    {
        public Task<List<RatePairs>> GateMoneyRateAsync(List<string> moneypairs);
        public Dictionary<string, double[]> GetRatePairsMinAndMaxDependingOnTheDate(List<string> moneypairs);
        public Task SaveMoneyRateAsync(List<RatePairs> ratePairsSave);
    }
}
