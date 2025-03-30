using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Konecta.API.Services
{
    public class HttpDataWriterService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpDataWriterService> _logger;

        public HttpDataWriterService(IHttpClientFactory httpClientFactory, ILogger<HttpDataWriterService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();

                    // Example: Call a public API (replace with yours)
                    var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts", stoppingToken);

                    var content = await response.Content.ReadAsStringAsync(stoppingToken);

                    // Save the data to a file
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data-output.json");
                    await File.WriteAllTextAsync(filePath, content, Encoding.UTF8, stoppingToken);

                    _logger.LogInformation("✅ Data written to file at {time}", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Failed to fetch or write data");
                }

                // Wait for 2 minutes before next request
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }
    }
}
