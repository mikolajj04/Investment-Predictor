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

            var prompt = $@"
                          Jesteś głównym analitykiem makroekonomicznym Wall Street. Otrzymujesz zestawienie 10 najważniejszych wiadomości globalnych o największym ładunku emocjonalnym (sentyment giełdowy).
                          Twoim zadaniem jest stworzenie profesjonalnego, ustrukturyzowanego podsumowania dla inwestorów.

                          Wymogi formatowania:
                          1. BEZWZGLĘDNIE używaj formatowania Markdown do strukturyzacji tekstu (używaj `**` do pogrubień kluczowych firm/wniosków oraz `*` do tworzenia list wypunktowanych).
                          2. ZABRONIONE jest używanie jakichkolwiek znaczników HTML (żadnych tagów typu <b>, <p>, <br>).
                          3. Rozpocznij od jednego, mocnego krótkiego streszczenia (Maksymalnie 4 zdania) podsumowującego ogólny nastrój na globalnych rynkach 
                          4. Podziel analizę na wyraźne kategorie, używając wypunktowań dla każdego sektora obecnego w wiadomościach (np. 🌍 Makroekonomia, 💡 Technologia, 🛢️ Energetyka, 💰 Finanse).
                          5. Pisz treściwie. Wymieniaj nazwy firm, zjawiska i kierunek zmian. Zero lania wody.
                          6. Zignoruj artykuły, które nie wnoszą wartościowej wiedzy inwestycyjnej.
                          7. Zwieńcz artykuł sekcją „🎯 **Kluczowe rekomendacje dla inwestora**”. W formie wypunktowanej listy przedstaw strategiczne rady oparte na powyższej syntezie. Każdą radę sformułuj troche luźniej i bardziej prosto niż powyższe punkty (np. „Co warto monitorować”, „Gdzie szukać przewagi”).
                          8. Użyj poziomej linii Markdown (czyli `---`) DOKŁADNIE DWA RAZY: raz po zakończeniu wstępu, a drugi raz tuż przed sekcją z rekomendacjami.
                          {string.Join("\n", articles)}";

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
