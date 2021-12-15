using OWLNEXT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Business.Contract
{
    public interface IMoneyService
    {
        public Task<List<string>> ListOfMoney();
    }
}
