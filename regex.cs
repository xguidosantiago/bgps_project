using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bgpSearch
{
    public class regex
    {
        // IPv4 con máscara CIDR (0–32)
        private static readonly Regex ipv4ConMascaraRegex = new Regex(
            @"^((25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})(\.(25[0-5]|2[0-4][0-9]|1?[0-9]{1,2})){3})\/(3[0-2]|[12]?[0-9])$",
            RegexOptions.Compiled);


        private static readonly Regex ipv6Regex = new Regex(
       @"^(([0-9A-Fa-f]{1,4}:){7}[0-9A-Fa-f]{1,4}|(([0-9A-Fa-f]{1,4}:){1,7}:)|(([0-9A-Fa-f]{1,4}:){1,6}:[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){1,5}(:[0-9A-Fa-f]{1,4}){1,2})|(([0-9A-Fa-f]{1,4}:){1,4}(:[0-9A-Fa-f]{1,4}){1,3})|(([0-9A-Fa-f]{1,4}:){1,3}(:[0-9A-Fa-f]{1,4}){1,4})|(([0-9A-Fa-f]{1,4}:){1,2}(:[0-9A-Fa-f]{1,4}){1,5})|([0-9A-Fa-f]{1,4}:)((:[0-9A-Fa-f]{1,4}){1,6})|(:((:[0-9A-Fa-f]{1,4}){1,7}|:)))(\/(12[0-8]|1[01][0-9]|[1-9]?[0-9]))?$",
       RegexOptions.Compiled);

        private static readonly Regex asnRegex = new Regex(@"^(AS)?([1-9][0-9]{0,9})$",RegexOptions.Compiled);

        // Regex solo máscara IPv4
        private static readonly Regex mascaraIpv4Regex = new Regex(
            @"^\/([0-9]|[1-2][0-9]|3[0-2])$",
            RegexOptions.Compiled);

        // Regex solo máscara IPv6
        private static readonly Regex mascaraIpv6Regex = new Regex(
            @"^\/([0-9]|[1-9][0-9]|1[0-1][0-9]|12[0-8])$",
            RegexOptions.Compiled);

        public static bool validarV4(string input)
        {
            return (ipv4ConMascaraRegex.IsMatch(input));
        }
        public static bool validarV6(string input)
        {
            return (ipv6Regex.IsMatch(input));
        }
        public static bool validarASN(string input)
        {
            return (asnRegex.IsMatch(input));
        }


    }
}
