using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bgpSearch
{
    public class bgpASN
    {
        public int asn { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string country { get; set; }
        public string website { get; set; }
        public List<ipv4Prefix> lstPrefixesV4 { get; set; }
        public List<ipv6Prefix> lstPrefixesV6 { get; set; }
        public List<bgpPeer> lstPeers { get; set; }
    }

}
