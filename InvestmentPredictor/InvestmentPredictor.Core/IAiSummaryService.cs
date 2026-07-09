using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPredictor.Core
{
    public interface IAiSummaryService
    {
        Task<string> GenerateMarketSummaryAsync(List<string> articles);
    }
}
