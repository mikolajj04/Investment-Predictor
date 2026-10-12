using InvestmentPredictor.Core.DTOs;
using InvestmentPredictor.Core;
using System.Text;
namespace InvestmentCalculator.WebApp.Services
{
  

    public class MarketNewsService : IMarketNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MarketNewsService> _logger;
        private readonly string _apiKey;
        public MarketNewsService(HttpClient httpClient, IConfiguration config, ILogger<MarketNewsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = config["Finnhub:ApiKey"] ?? throw new InvalidOperationException("Finnhub:ApiKey is missing in configuration!");
        }



        public async Task<string?> GetMarketNewsSummaryAsync()
        {

            var requestUrl = $"news?category=general&token={_apiKey}";
            try
            {
                var newsItems = await _httpClient.GetFromJsonAsync<List<FinnhubNewsItem>>(requestUrl);

                if (newsItems == null || newsItems.Count == 0)
                {
                    _logger.LogWarning("Finnhub returned an empty news list. Skipping this cycle.");
                    return null;
                }

                var recentNews = newsItems.Take(30);
                var sb = new StringBuilder();
                foreach (var item in recentNews)
                {
                    sb.AppendLine($"- {item.Headline}: {item.Summary}");
                }

                return sb.ToString();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP Request error while fetching news from Finnhub.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error processing Finnhub data.");
                return null;
            }
        }



    }
}