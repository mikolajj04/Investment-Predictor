using System.Reflection;
using System.Text.Json;
using InvestmentPredictor.Core;
using InvestmentPredictor.Core.DTOs;

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

        public async Task<MarketSummaryResult> GenerateMarketSummaryAsync(List<string> articles)
        {
            if(articles == null || !articles.Any())
            {
                return new MarketSummaryResult("Brak danych do streszczenia", "No news to summarize");
            }

            var prompt = $@"
                          Jesteś głównym analitykiem makroekonomicznym Wall Street. Otrzymujesz zestawienie najważniejszych wiadomości globalnych o największym ładunku emocjonalnym (sentyment giełdowy).
                          Twoim zadaniem jest stworzenie profesjonalnego, ustrukturyzowanego podsumowania dla inwestorów w DWÓCH wersjach językowych: polskiej i angielskiej.

                          Wymogi formatowania:
                          1. BEZWZGLĘDNIE używaj formatowania Markdown do strukturyzacji tekstu (używaj `**` do pogrubień kluczowych firm/wniosków oraz `*` do tworzenia list wypunktowanych).
                          2. ZABRONIONE jest używanie jakichkolwiek znaczników HTML (żadnych tagów typu <b>, <p>, <br>).
                          3. Rozpocznij od jednego, mocnego krótkiego streszczenia (około 4 zdania) podsumowującego ogólny nastrój na globalnych rynkach, po czym przejdź do dalszej analizy.
                          4. Podziel analizę na wyraźne kategorie tematyczne: 
                             - W wersji polskiej używaj nagłówków w stylu: 🌍 Makroekonomia, 💡 Technologia, 💰 Finanse.
                             - W wersji angielskiej używaj ich odpowiedników: 🌍 Macroeconomics, 💡 Technology, 💰 Finance.
                          5. Pisz treściwie, bez lania wody. Wymieniaj nazwy firm, zjawiska i kierunek zmian. Nie tłumacz nazw własnych firm.
                          6. Zignoruj artykuły, które nie wnoszą wartościowej wiedzy inwestycyjnej.
                          7. Zwieńcz artykuł sekcją rekomendacji:
                            - W wersji polskiej nagłówek to: „🎯 **Kluczowe rekomendacje dla inwestora**”, a punkty zacznij od np. „Gdzie szukać przewagi:”, „Co warto monitorować:”, „Czego unikać:”.
                            - W wersji angielskiej nagłówek to: „🎯 **Key Takeaways for Investors**”, a punkty zacznij od np. „Where to find an edge:”, „What to monitor:”, „What to avoid:”.
                           Sekcję rekomendacji sformułuj troche luźniej i bardziej prosto i bezpośrednio w porównaniu do całego streszczenia.
                          8. Użyj poziomej linii Markdown (czyli `---`) DOKŁADNIE DWA RAZY: raz po zakończeniu wstępu, a drugi raz tuż przed sekcją z rekomendacjami.
                          
                          ODPOWIEDZ WYŁĄCZNIE CZYSTYM OBIEKTEM JSON (bez znaczników markdown typu ```json):
                          {{
                            ""summaryPl"": ""...treść markdown po polsku..."",
                            ""summaryEn"": ""...treść markdown po angielsku...""
                          }}

                          Oto Wiadomości:
                          {string.Join("\n", articles)}";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                },
                generationConfig = new
                {
                    responseMimeType = "application/json"
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
            var docOptions = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            var rawText = jsonResponse
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text").GetString();

            using var doc = JsonDocument.Parse(rawText!, docOptions);
            var pl = doc.RootElement.GetProperty("summaryPl").GetString() ?? string.Empty;
            var en = doc.RootElement.GetProperty("summaryEn").GetString() ?? string.Empty;

            return new MarketSummaryResult(pl, en);

        }
        
    }
}
