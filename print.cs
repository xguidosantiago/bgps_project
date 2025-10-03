using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bgpSearch
{
    public class print
    {
        public static void ipInvalida()
        {
            Console.WriteLine("> Error: La direccion/mascara ingresada no es correcta\n");
        }
        public static void asnInvalido()
        {
            Console.WriteLine("> Error: El ASN ingresado no es correcto\n");
        }
        public static void MostrarAyuda()
        {
            Console.WriteLine("Uso:");
            Console.WriteLine("  -p <prefijo>     : Búsqueda por prefijo IPv4/IPv6 (ej: -p 8.0.0.0/16 ó -p 2001:db8::1/64)");
            Console.WriteLine("  -a <ASN>         : Búsqueda por ASN (ej: -a 22927)");
            Console.WriteLine("  -r <ASN>         : Búsqueda por prefijos del ASN (ej: -r 22927)");
            Console.WriteLine("  -e <ASN>         : Búsqueda por peers del ASN (ej: -e 22927)");
        }
        public static void mostrarIP(IpPrefix ipData)
        {
            Console.WriteLine("IP Prefix Info");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"{"Prefijo:",-14} {ipData.prefix}");
            Console.WriteLine($"{"IP:",-14} {ipData.ip}");
            Console.WriteLine($"{"Máscara:",-14} {ipData.cidr}");
            Console.WriteLine($"{"AS:",-14} {ipData.asn}");
            Console.WriteLine($"{"Nombre:",-14} {ipData.name}");
            Console.WriteLine($"{"Descripción:",-14} {ipData.description}");
            Console.WriteLine($"{"País:",-14} {ipData.country}");
            Console.WriteLine();
        }

        public static void MostrarAsn(bgpASN asn)
        {
            Console.WriteLine("ASN Info");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"{"ASN:",-14} {asn.asn}");
            Console.WriteLine($"{"Nombre:",-14} {asn.name}");
            Console.WriteLine($"{"Descripción:",-14} {asn.description}");
            Console.WriteLine($"{"País:",-14} {asn.country}");
            Console.WriteLine($"{"Website:",-14} {asn.website}");
            Console.WriteLine();
        }

        public static void mostrarPrefijosPorASN(bgpASN asn)
        {
            Console.WriteLine($"Resultado para AS {asn.asn} \n");
            // 🔹 IPv4
            if (asn.lstPrefixesV4 != null && asn.lstPrefixesV4.Count > 0)
            {
                Console.WriteLine("IPv4 Prefixes");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Prefijo",-20} {"Máscara",-8} {"Nombre",-50} {"País",-5}");
                Console.WriteLine("------------------------------------------------------------------------------------------");

                foreach (var p in asn.lstPrefixesV4)
                {
                    Console.WriteLine($"{p.prefix,-20} {p.cidr,-8} {Truncar(p.name,50),-50} {p.country,-5}");
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No se encontraron prefijos IPv4.\n");
            }

            // 🔹 IPv6
            if (asn.lstPrefixesV6 != null && asn.lstPrefixesV6.Count > 0)
            {
                Console.WriteLine("IPv6 Prefixes");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Prefijo",-20} {"Máscara",-8} {"Nombre",-50} {"País",-5}");
                Console.WriteLine("------------------------------------------------------------------------------------------");

                foreach (var p in asn.lstPrefixesV6)
                {
                    Console.WriteLine($"{p.prefix,-20} {p.cidr,-8} {Truncar(p.name,50),-50} {p.country,-5}");
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No se encontraron prefijos IPv6.\n");
            }
        }

        public static void mostrarPeerPorASN(bgpASN asn)
        {
            Console.WriteLine($"Resultado para AS {asn.asn} \n");
            if (asn.lstPeers != null && asn.lstPeers.Count > 0)
            {
                Console.WriteLine("IPv4 Peers");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"AS",-15} {"Nombre",-60} {"País",-5}");
                Console.WriteLine("------------------------------------------------------------------------------------------");

                foreach (var p in asn.lstPeers)
                {
                    Console.WriteLine($"{p.asn,-15} {Truncar(p.name,60),-60} {p.country,-5}");
                }

                Console.WriteLine();
            }
        }

        private static string Truncar(string texto, int maxLen)
        {
            if (texto == null || texto == "")
            {
                return texto;
            }
            else
            {
                if (texto.Length > maxLen)
                {
                    return texto.Substring(0, maxLen - 3) + "...";
                }
                else
                {
                    return texto;
                }
            }
        }
    }
}

