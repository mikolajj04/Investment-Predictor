using Microsoft.EntityFrameworkCore;
using InvestmentPredictor.Core.Entities;

namespace InvestmentCalculator.WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MarketSnippet> MarketSnippets { get; set; }
    }
}

    