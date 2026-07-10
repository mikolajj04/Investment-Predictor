using System.Reflection;
using System.Text.Json;
using InvestmentPredictor.Core;

namespace InvestmentCalculator.WebApp.Services
{
    public class GeminiSummaryService : IAiSummaryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiSummaryService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["Gemini:ApiKey"] ?? throw new InvalidOperationException("Gemini ApiKey is missing from configuration!");

        }

        public async Task<string> GenerateMarketSummaryAsync(List<string> articles)
        {
            if(articles == null || !articles.Any())
            {
                return "No news to summarize.";
            }

            var prompt = "Jesteś analitykiem finansowym. Poniżej masz listę najnowszych nagłówków i krótkich opisów z rynku giełdowego. " +
                         "Twoim zadaniem jest napisanie jednego, profesjonalnego, bardzo spójnego podsumowania (maksymalnie 4-5 zdania). Opisania sytuacji profesjonalnym ale zrozumiałym językiem. " +
                         "w języku polskim, które oceni ogólny sentyment na rynku.\n\n" +
                         string.Join("\n- ", articles);

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-lite-latest:generateContent?key={_apiKey}";
            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
            if (!response.IsSuccessStatusCode)
            {
      
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Gemini Error: {response.StatusCode} - details: {errorContent}");
            }
            var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

            var summary = jsonResponse
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text").GetString();

            return summary ?? "Something went wrong... no summary generated.";

        }
        
    }
}
