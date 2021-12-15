using OWLNEXT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Repository.Contracts
{
    public interface IMoneyRepository
    {
       public Task<List<string>> ListOfMoney();
    }
}
