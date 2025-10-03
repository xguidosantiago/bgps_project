using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace bgpSearch
{
    public static class query
    {

        public static async Task<ipv4Prefix> ipv4QueryAsync(string prefix)
        {
            string url = $"https://api.bgpview.io/prefix/{prefix}";
            ipv4Prefix ipv4Data = new ipv4Prefix();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp");
                string response = await client.GetStringAsync(url);

                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    JsonElement root = doc.RootElement;


                    string status = root.GetProperty("status").GetString();
                    if (status != "ok")
                    {
                        Console.WriteLine("Error: La dirección ingresada no es válida. Debe ingresar una IP de red válida con su máscara.");
                        return null; // Devuelve null para que el Main lo maneje
                    }

                    JsonElement data = root.GetProperty("data");

                    ipv4Data.prefix = data.GetProperty("prefix").GetString();
                    ipv4Data.ip = data.GetProperty("ip").GetString();
                    ipv4Data.cidr = data.GetProperty("cidr").GetInt16();
                    ipv4Data.name = data.GetProperty("name").GetString();

                    JsonElement asns = data.GetProperty("asns");
                    foreach (JsonElement asn in asns.EnumerateArray())
                    {
                        ipv4Data.description = asn.GetProperty("description").GetString();
                        ipv4Data.country = asn.GetProperty("country_code").GetString();
                    }
                }
            }

            return ipv4Data;
        }

        public static async Task<bgpASN> bgpAsnQueryAsync(string asNumber)
        {

            string url = $"https://api.bgpview.io/asn/{asNumber}";
            bgpASN bgpASN = new bgpASN();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp");
                string response = await client.GetStringAsync(url);

                // Analizo el JSON como un objeto dinámico
                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement data = root.GetProperty("data");

                    bgpASN.asn = data.GetProperty("asn").GetInt32();
                    bgpASN.name = data.GetProperty("name").GetString();
                    bgpASN.description = data.GetProperty("description_short").GetString();
                    bgpASN.country = data.GetProperty("country_code").GetString();
                    bgpASN.website = data.GetProperty("website").GetString();
                }
            }
            return bgpASN;
        }

        public static async Task<bgpASN> asnPrefixesQueryAsync(string asNumber)
        {

            string url = $"https://api.bgpview.io/asn/{asNumber}/prefixes";
            bgpASN bgpASN = new bgpASN();
            bgpASN.lstPrefixesV4 = new List<ipv4Prefix>();
            bgpASN.lstPrefixesV6 = new List<ipv6Prefix>();
            bgpASN.asn = int.Parse(asNumber);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp");
                string response = await client.GetStringAsync(url);

                // Analizo el JSON como un objeto dinámico
                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement data = root.GetProperty("data");

                    if (data.TryGetProperty("ipv4_prefixes", out JsonElement ipv4_prefixes))
                    {
                        foreach (JsonElement prefix in ipv4_prefixes.EnumerateArray())
                        {
                            ipv4Prefix ipv4Data = new ipv4Prefix
                            {
                                prefix = prefix.GetProperty("prefix").GetString(),
                                ip = prefix.GetProperty("ip").GetString(),
                                cidr = prefix.GetProperty("cidr").GetInt32(),
                                name = prefix.GetProperty("name").GetString(),
                                description = prefix.GetProperty("description").GetString(),
                                country = prefix.GetProperty("country_code").GetString()
                            };

                            bgpASN.lstPrefixesV4.Add(ipv4Data);
                        }
                    }
                    if (data.TryGetProperty("ipv6_prefixes", out JsonElement ipv6_prefixes))
                    {
                        foreach (JsonElement prefix in ipv6_prefixes.EnumerateArray())
                        {
                            ipv6Prefix ipv6Data = new ipv6Prefix
                            {
                                prefix = prefix.GetProperty("prefix").GetString(),
                                ip = prefix.GetProperty("ip").GetString(),
                                cidr = prefix.GetProperty("cidr").GetInt32(),
                                name = prefix.GetProperty("name").GetString(),
                                description = prefix.GetProperty("description").GetString(),
                                country = prefix.GetProperty("country_code").GetString()
                            };

                            bgpASN.lstPrefixesV6.Add(ipv6Data);
                        }
                    }
                }
            }
            return bgpASN;
        }
        public static async Task<bgpASN> bgpPeerQueryAsync(string asNumber)
        {

            string url = $"https://api.bgpview.io/asn/{asNumber}/peers";
            bgpASN bgpASN = new bgpASN();
            bgpASN.asn = int.Parse(asNumber);
            bgpASN.lstPeers = new List<bgpPeer>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp");
                string response = await client.GetStringAsync(url);

                // Analizo el JSON como un objeto dinámico
                using (JsonDocument doc = JsonDocument.Parse(response))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement data = root.GetProperty("data");
                    JsonElement peers = data.GetProperty("ipv4_peers");
                    foreach (JsonElement p in peers.EnumerateArray())
                    {
                        bgpPeer peer = new bgpPeer();
                        peer.asn = p.GetProperty("asn").GetInt32();
                        peer.name = p.GetProperty("name").GetString();
                        peer.country = p.GetProperty("country_code").GetString();
                        bgpASN.lstPeers.Add(peer);
                    }
                }
            }
            return bgpASN;
        }

    }
}
