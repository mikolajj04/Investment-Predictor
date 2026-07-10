using InvestmentCalculator.WebApp.Data;
using InvestmentPredictor.Core;
using InvestmentPredictor.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestmentCalculator.WebApp.Services
{
    public class MarketSummaryWorker : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MarketSummaryWorker> _logger;

        public MarketSummaryWorker(IServiceProvider serviceProvider, ILogger<MarketSummaryWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            

            _logger.LogInformation("The Night AI Orchestrator has begun its work.");
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRun = now.Date.AddHours(3);
                if (now >= nextRun)
                {
                    nextRun = nextRun.AddDays(1);
                }
                var delay = nextRun - now;
                _logger.LogInformation($"Orkiestrator went to sleep. Next wake-up in: {delay.TotalHours:F2} hours | on: {nextRun}.");
                await Task.Delay(delay, stoppingToken);
                try
                {
                    _logger.LogInformation("Orchestrator is waking up. Generating summary...");
                    using var scope = _serviceProvider.CreateScope();
                    var newsService = scope.ServiceProvider.GetRequiredService<IMarketNewsService>();
                    var aiService = scope.ServiceProvider.GetRequiredService<IAiSummaryService>();
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    //var rawNews = await newsService.GetMarketNewsAsync();
                    //var topArticles = rawNews
                    //    .Where(n=>Math.Abs(n.SentimentScore)>=0.15)
                    //    .OrderByDescending(n => Math.Abs(n.SentimentScore))
                    //    .Take(15)
                    //    .Select(n => $"{n.Title}: {n.Summary}")
                    //    .ToList();

                    //MOCK Articles
                    var topArticles = new List<string>
                    {
                        "Makroekonomia: Rezerwa Federalna (Fed) decyduje o pozostawieniu stóp procentowych bez zmian, co stabilizuje rynki finansowe.",
                        "Energia: Ceny ropy naftowej brent spadają o 3% z powodu zwiększonego wydobycia w krajach OPEC.",
                        "Finanse: Europejskie banki odnotowują rekordowe zyski w pierwszym kwartale dzięki wyższym marżom odsetkowym.",
                        "Technologia: Gemini Flash-Lite rewolucjonizuje rynek systemów automatyzacji backendu w chmurze."
                    };

                    if (topArticles.Any())
                    {
                        _logger.LogInformation($"Sending {topArticles.Count} articles to AI brain...");
                        var aiSummary = await aiService.GenerateMarketSummaryAsync(topArticles);

                        //MOCK Gemini
                        //var aiSummary = "🤖 [Mock AI]: Rynek technologiczny w fazie silnej korekty. Akcje Tesli nieznacznie tracą na wartości w obliczu nowych danych produkcyjnych, natomiast Microsoft kontynuuje stabilny wzrost napędzany gigantycznym popytem na usługi chmurowe Azure i rozwiązania sztucznej inteligencji.";


                        var newSnippet = new MarketSnippet()
                        {
                            Content = aiSummary,
                            SummaryType = "Daily",
                            CreatedAt = DateTime.UtcNow

                        };

                        dbContext.MarketSnippets.Add(newSnippet);
                        await dbContext.SaveChangesAsync();
                        _logger.LogInformation("Success! Summary generated and saved in database.");
                    }
                    else
                    {
                        _logger.LogWarning("No news from AlphaVantage. Terminating process.");
                    }

                }catch(Exception ex)
                {
                    _logger.LogError(ex, "Critical Error in Night AI Orchestrator");
                }

                
               
            }
        }

            
    }
}
