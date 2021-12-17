using OWLNEXT.Business.Contract;
using OWLNEXT.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Business
{
    public class MoneyService : IMoneyService
    {
        private readonly IMoneyRepository _moneyRepository;

        public MoneyService( IMoneyRepository moneyRepository)
        {
            _moneyRepository = moneyRepository;
        }

        public Task<List<string>> ListOfMoney()
        {
            return _moneyRepository.ListOfMoney();
        }
    }
}
