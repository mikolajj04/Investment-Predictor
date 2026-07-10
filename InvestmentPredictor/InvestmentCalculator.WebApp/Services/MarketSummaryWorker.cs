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
                try
                {
                    _logger.LogInformation("Orchestrator is waking up. Generating summary...");
                    using var scope = _serviceProvider.CreateScope();
                    var newsService = scope.ServiceProvider.GetRequiredService<IMarketNewsService>();
                    var aiService = scope.ServiceProvider.GetRequiredService<IAiSummaryService>();
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    //var rawNews = await newsService.GetMarketNewsAsync();
                    //var topArticles = rawNews
                    //    .OrderByDescending(n => n.SentimentScore)
                    //    .Take(10)
                    //    .Select(n => $"{n.Title}: {n.Summary}")
                    //    .ToList();

                    //MOCK Articles
                    var topArticles = new List<string>
                    {
                        "Apple: Apple ogłosiło upadłość!",
                        "Tesla: Elon Musk z niewiadomego powodu masowo zwalnia pracowników.",
                        "Microsoft: AI spowodowało wyciek poufnych danych klientów!"
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

                _logger.LogInformation("The Night AI Orchestrator will sleep for 2 minutes");
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }

            
    }
}
