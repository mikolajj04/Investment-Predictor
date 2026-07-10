using InvestmentPredictor.Core.DTOs;
using InvestmentPredictor.Core;
namespace InvestmentCalculator.WebApp.Services
{
  

    public class MarketNewsService : IMarketNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public MarketNewsService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["AlphaVantage:ApiKey"] ?? throw new InvalidOperationException("AlphaVantage ApiKey is missing from configuration!");
        }



        public async Task<List<NewsArticles>> GetMarketNewsAsync()
        {

            var url = $"query?function=NEWS_SENTIMENT&topics=economy_macro,financial_markets,energy_transportation,technology&sort=LATEST&apikey={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<AlphaVantageNewsResponse>(url);
            return response?.Feed ?? new List<NewsArticles>();
        }



    }
}