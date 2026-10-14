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

        public async Task<MarketSummaryResult> GenerateMarketSummaryAsync(string articles)
        {
            if(articles == null || !articles.Any())
            {
                return new MarketSummaryResult("Brak danych do streszczenia", "No news to summarize");
            }

            var prompt = $@"
                          Jesteś głównym analitykiem makroekonomicznym Wall Street. Twoim zadaniem jest stworzenie profesjonalnego, ustrukturyzowanego podsumowania dla inwestorów na podstawie dostarczonych surowych wiadomości.

                          Wymogi formatowania:
                          1. BEZWZGLĘDNIE używaj formatowania Markdown do strukturyzacji tekstu (używaj `**` do pogrubień kluczowych firm/wniosków oraz `*` do tworzenia list wypunktowanych).
                          2. ZABRONIONE jest używanie jakichkolwiek znaczników HTML (żadnych tagów typu <b>, <p>, <br>).
                          3. WSTĘP: Rozpocznij od jednego, mocnego krótkiego streszczenia (około 4 zdania) podsumowującego ogólny nastrój na globalnych rynkach, po czym BEZWZGLĘDNIE przejdź do dalszej analizy.
                          4. ROZWINIĘCIE (Kategorie tematyczne): Podziel analizę na wyraźne kategorie tematyczne używając nagłówków trzeciego stopnia w Markdown (###):: 
                             - W wersji polskiej używaj nagłówków w stylu: `### 🌍 Makroekonomia`, `### 💡 Technologia`, `### 💰 Finanse`. 
                             - W wersji angielskiej używaj ich odpowiedników: `### 🌍 Macroeconomics`, `### 💡 Technology`, `### 💰 Finance`.
                             - Wyżej wymienione nagłówki to przykłady, sam dopasuj od 2 do 5 NAJWAŻNIEJSZYCH obszarów tematycznych na dany dzień. Dopasuj do nich odpowiednie emotikony i oznacz nagłówkiem trzeciego stopnia (###). (Kolejne przykłady: `### 🛢️ Rynek Ropy i Surowców`, `### 🤖 AI i Półprzewodniki`, `### 🏦 Decyzje Banków Centralnych`, i tak dalej...) -  Reaguj na to, czym faktycznie żyje rynek.
                          5. Pisz treściwie, bez lania wody. Łącz kropki i agreguj powiązane informacje z różnych źródeł w spójną narrację. Wymieniaj nazwy firm, zjawiska i kierunek zmian. Nie tłumacz nazw własnych firm. W części po Polsku (reportPl) unikaj kalek z języka angielskiego oraz zadbaj o poprawną polską składnię.
                          6. Zignoruj artykuły, które nie wnoszą wartościowej wiedzy inwestycyjnej. BEZWZGLĘDNIE weryfikuj spójność logiczną i merytoryczną faktów, zanim umieścisz je w raporcie. Niedopuszczalne są wewnętrzne sprzeczności w wygenerowanym tekście. Jeśli dostarczone wiadomości są ubogie (np. sesja weekendowa) lub nieistotne, wprost to zaznacz, unikając sztucznego pompowania paniki lub optymizmu.
                          7. ZAKOŃCZENIE (Kluczowe obserwacje): Zwieńcz artykuł sekcją kluczowych obserwacji rynkowych:
                            - W wersji polskiej nagłówek to: `### 🎯 **Kluczowe obserwacje rynkowe**`, a punkty zacznij od np. „Gdzie szukać przewagi:”, „Co warto monitorować:”, „Czego unikać:”.
                            - W wersji angielskiej nagłówek to: `### 🎯 **Market Observations & Takeaways**`, a punkty zacznij od np. „Where to find an edge:”, „What to monitor:”, „What to avoid:”.
                           Sekcję kluczowych obserwacji sformułuj troche luźniej i bardziej prosto i bezpośrednio w porównaniu do całego streszczenia.
                          8. Użyj poziomej linii Markdown (czyli `---`) DOKŁADNIE DWA RAZY: raz po zakończeniu wstępu, a drugi raz tuż przed sekcją z kluczowymi obserwacjami.
                            ABSOLUTNIE KLUCZOWE: Musisz zawsze zostawić jedną PUSTĄ LINIĘ przed i po `---`. Nigdy nie doklejaj myślników bezpośrednio pod tekstem!

                          ODPOWIEDZ WYŁĄCZNIE CZYSTYM OBIEKTEM JSON (bez znaczników markdown typu ```json). Używamy kluczy 'reportPl' i 'reportEn', aby wymusić pełną formę raportu:
                          {{
                            ""reportPl"": ""...pełny tekst markdown po polsku..."",
                            ""reportEn"": ""...pełny tekst markdown po angielsku...""
                          }}

                          Oto Wiadomości:
                          {articles}";

            var requestBody = new
            {
                contents = new[]
     {
        new { parts = new[] { new { text = prompt } } }
    },
                generationConfig = new
                {
                    temperature = 0.4,
                    responseMimeType = "application/json",
                    responseSchema = new
                    {
                        type = "OBJECT",
                        properties = new
                        {
                            reportPl = new { type = "STRING" },
                            reportEn = new { type = "STRING" }
                        },
                        required = new[] { "reportPl", "reportEn" }
                    },
                    thinkingConfig = new
                    {
                        thinkingBudget = 512
                    }
                }
            };

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.8-flash:generateContent?key={_apiKey}";
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
            var pl = doc.RootElement.GetProperty("reportPl").GetString() ?? string.Empty;
            var en = doc.RootElement.GetProperty("reportEn").GetString() ?? string.Empty;

            return new MarketSummaryResult(pl, en);

        }
        
    }
}
