using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Entity
{
    public class RateMoney
    {
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public string ErrorMessage { get; set; }
        public RatePairs Rate { get; set; }
    }
}
