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
                var polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                var nowInPoland = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, polandTimeZone);
                _logger.LogInformation($"Orchestrator checks summary status. Current time in Poland: {nowInPoland}");
                    if (nowInPoland.Hour > 8 || (nowInPoland.Hour == 8 && nowInPoland.Minute >= 30))
                    {


                        using var scope = _serviceProvider.CreateScope();
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        var todayUtc = DateTime.UtcNow.Date;
                        bool alreadyExists = await dbContext.MarketSnippets
                         .AnyAsync(s => s.CreatedAt.Date == todayUtc, stoppingToken);

                        if (!alreadyExists)
                        {
                            var newsService = scope.ServiceProvider.GetRequiredService<IMarketNewsService>();
                            var aiService = scope.ServiceProvider.GetRequiredService<IAiSummaryService>();

                            var newsTextContext = await newsService.GetMarketNewsSummaryAsync();
                            if (!string.IsNullOrWhiteSpace(newsTextContext)) { 

                            //---MOCK Articles----
                            //var topArticles = new List<string>
                            //{
                            //    "Makroekonomia: Rezerwa Federalna (Fed) decyduje o pozostawieniu stóp procentowych bez zmian, co stabilizuje rynki finansowe.",
                            //    "Energia: Ceny ropy naftowej brent spadają o 3% z powodu zwiększonego wydobycia w krajach OPEC.",
                            //    "Finanse: Europejskie banki odnotowują rekordowe zyski w pierwszym kwartale dzięki wyższym marżom odsetkowym.",
                            //    "Technologia: Gemini Flash-Lite rewolucjonizuje rynek systemów automatyzacji backendu w chmurze."
                            //};

                            
                                _logger.LogInformation("Sending news text payload to AI brain...");
                                var aiSummary = await aiService.GenerateMarketSummaryAsync(newsTextContext);

                                //---MOCK Gemini----
                                //var aiSummary = "🤖 [Mock AI]: Rynek technologiczny w fazie silnej korekty. Akcje Tesli nieznacznie tracą na wartości w obliczu nowych danych produkcyjnych, natomiast Microsoft kontynuuje stabilny wzrost napędzany gigantycznym popytem na usługi chmurowe Azure i rozwiązania sztucznej inteligencji.";


                                var newSnippet = new MarketSnippet()
                                {
                                    Content = aiSummary.PolishSummary,
                                    ContentEn = aiSummary.EnglishSummary,
                                    SummaryType = "Daily",
                                    CreatedAt = DateTime.UtcNow

                                };

                                dbContext.MarketSnippets.Add(newSnippet);
                                await dbContext.SaveChangesAsync(stoppingToken);
                                _logger.LogInformation("Success! Summary generated and saved in database.");
                            }
                            else
                            {
                                _logger.LogWarning("No news from Finhub or API error. Terminating process for this cycle.");
                            }

                        }
                        else
                        {
                            _logger.LogInformation("Report for today already exists. Orchestrator skips generation. ");
                        }

                    }

                }catch(Exception ex)
                {
                    _logger.LogError(ex, "Critical Error in AI Orchestrator");
                }

                _logger.LogInformation("Orchestrator went to sleep for next 10 minutes");
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);


            }
        }

            
    }
}
