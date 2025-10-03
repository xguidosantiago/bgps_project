using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bgpSearch
{
    public abstract class ipPrefix
    {
        public string prefix { get; set; }
        public string ip { get; set; }
        public int cidr { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string country { get; set; }
    }
}
