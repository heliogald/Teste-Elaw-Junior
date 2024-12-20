using Newtonsoft.Json;
using WebCrawler.Models;
using WebCrawler.Services;

namespace WebCrawler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string baseUrl = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc";
            var proxyScraper = new ProxyScraper();
            var databaseLogger = new DatabaseLogger("executionLogs.db");
            var allProxies = new List<ProxyInfo>();

            // Inicializar banco de dados
            databaseLogger.InitializeDatabase();

            DateTime startTime = DateTime.Now;
            int totalPages = 0;

            while (true)
            {
                string url = $"{baseUrl}?page={totalPages + 1}";
                var proxies = await proxyScraper.ScrapePageAsync(url);

                if (proxies.Count == 0) break;

                allProxies.AddRange(proxies);
                totalPages++;

                // Salvar HTML da página
                File.WriteAllText($"page_{totalPages}.html", await new HttpClient().GetStringAsync(url));

                // Execução assíncrona com controle de threads
                await Utils.ExecuteWithLimitedThreads(async () =>
                {
                    await proxyScraper.ScrapePageAsync(url);
                });
            }

            // Salvar dados em JSON
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Result.json");
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(allProxies, Formatting.Indented));

            // Criar log de execução
            DateTime endTime = DateTime.Now;
            var executionLog = new ExecutionLog
            {
                StartTime = startTime,
                EndTime = endTime,
                TotalPages = totalPages,
                TotalRows = allProxies.Count,
                JsonFilePath = jsonFilePath
            };

            // Registrar no banco de dados
            databaseLogger.LogExecution(executionLog);

            Console.WriteLine("Crawling finished!");
        }
    }
}
