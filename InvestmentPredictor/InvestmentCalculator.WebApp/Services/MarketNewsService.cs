using InvestmentPredictor.Core.DTOs;
using InvestmentPredictor.Core;
namespace InvestmentCalculator.WebApp.Services
{
  

    public class MarketNewsService : IMarketNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public MarketNewsService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }



        public async Task<List<NewsArticles>> GetMarketNewsAsync()
        {
            var apiKey = _config["AlphaVantage:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("AlphaVantage ApiKey is missing from configuration!");
            }
            var url = $"query?function=NEWS_SENTIMENT&tickers=AAPL,TSLA,MSFT&apikey={apiKey}";
            var response = await _httpClient.GetFromJsonAsync<AlphaVantageNewsResponse>(url);
            return response?.Feed ?? new List<NewsArticles>();
        }



    }
}