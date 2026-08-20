using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestmentPredictor.Core.DTOs;

namespace InvestmentPredictor.Core
{
    public interface IAiSummaryService
    {
        Task<MarketSummaryResult> GenerateMarketSummaryAsync(List<string> articles);
    }
}
