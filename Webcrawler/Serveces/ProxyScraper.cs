using HtmlAgilityPack;
using WebCrawler.Models;

namespace WebCrawler.Services
{
    public class ProxyScraper
    {
        private static readonly HttpClient _client = new HttpClient();

        public async Task<List<ProxyInfo>> ScrapePageAsync(string url)
        {
            var proxies = new List<ProxyInfo>();

            try
            {
                var html = await _client.GetStringAsync(url);
                Console.WriteLine($"Página carregada: {url}");

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var rows = doc.DocumentNode.SelectNodes("//table//tr");
                if (rows != null)
                {
                    Console.WriteLine($"Página {url}: Encontradas {rows.Count - 1} linhas (excluindo o cabeçalho)");

                    foreach (var row in rows.Skip(1)) // Ignorar cabeçalho
                    {
                        var columns = row.SelectNodes("td");
                        if (columns != null && columns.Count >= 7)
                        {
                            string ipAddress = columns[1].InnerText.Trim();
                            string portText = columns[2].InnerText.Trim();
                            string country = columns[3].InnerText.Trim();
                            string protocol = columns[6].InnerText.Trim();

                            Console.WriteLine($"Extraído: IP = {ipAddress}, Porta = {portText}, País = {country}, Protocolo = {protocol}");

                            int port = 0;
                            if (string.IsNullOrEmpty(portText) || !int.TryParse(portText, out port))
                            {
                                port = -1; // Valor padrão para porta inválida
                            }

                            if (!string.IsNullOrEmpty(ipAddress) && !string.IsNullOrEmpty(country))
                            {
                                proxies.Add(new ProxyInfo
                                {
                                    IpAddress = ipAddress,
                                    Port = port,
                                    Country = country,
                                    Protocol = protocol
                                });
                            }
                            else
                            {
                                Console.WriteLine($"Proxy inválido: IP = {ipAddress}, Porta = {portText}, País = {country}, Protocolo = {protocol}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erro: Colunas insuficientes na linha.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Página {url}: Nenhuma linha encontrada.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar a página {url}: {ex.Message}");
            }

            // Verifique se algum proxy foi extraído
            Console.WriteLine($"Proxies extraídos da página {url}: {proxies.Count}");
            return proxies;
        }


    }
}
