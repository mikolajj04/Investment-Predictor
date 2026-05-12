using InvestmentPredictor.Core.DTOs;

namespace InvestmentPredictor.Core
{
    public interface IMarketNewsService
    {
        Task<List<NewsArticles>> GetMarketNewsAsync();
    }
}
