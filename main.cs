using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bgpSearch
{
    internal class main
    {
        static async Task Main(string[] args)
        {

            if (args.Length < 2)
            {
                print.MostrarAyuda();
                return;
            }
            else
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                string param = args[0];
                string consulta = args.Length > 1 ? args[1] : "";

                if (param == "-p")
                {
                    consulta = args[1];
                    if (regex.validarV4(consulta) || regex.validarV6(consulta))
                    {
                        ipv4Prefix ipv4 = await query.ipv4QueryAsync(args[1]);
                        if(ipv4 != null)
                        {
                            print.MostrarIpv4(ipv4);
                        }
                    }
                    else
                    {
                        print.ipInvalida();
                        print.MostrarAyuda();
                    }
                }

                else if (param == "-a")
                {
                    consulta = args[1];
                    if (regex.validarASN(consulta))
                    {
                        bgpASN asn = await query.bgpAsnQueryAsync(args[1]);
                        print.MostrarAsn(asn);
                    }
                    else
                    {
                        print.asnInvalido();
                        print.MostrarAyuda();
                    }
                }
                else if (param == "-r")
                {
                    consulta = args[1];
                    if (regex.validarASN(consulta))
                    {
                        bgpASN asn = await query.asnPrefixesQueryAsync(args[1]);
                        print.mostrarPrefijosPorASN(asn);
                    }
                    else
                    {
                        print.asnInvalido();
                        print.MostrarAyuda();
                    }
                }
                else if (param == "-e")
                {
                    consulta = args[1];
                    if (regex.validarASN(consulta))
                    {
                        bgpASN asn = await query.bgpPeerQueryAsync(args[1]);
                        print.mostrarPeerPorASN(asn);
                    }
                    else
                    {
                        print.asnInvalido();
                        print.MostrarAyuda();
                    }
                }
                else
                {
                    print.MostrarAyuda();
                }
            }
        }               
          
    }
}
