using OWLNEXT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Business.Contract
{
    public interface IRatePairsService
    {
        public Task<List<RatePairs>> RateMoney(List<string> moneypairs);
        public Dictionary<string, double[]> GetRatePairsMinAndMaxDependingOnTheDate(List<string> moneypairs);
    }
}
