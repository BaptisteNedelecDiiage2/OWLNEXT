using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWLNEXT.Entity
{
    public class Cross
    {
        public int Status { get; set; }
        public string Date { get; set; }
        public List<Money> CrossList { get; set; }
    }
}
