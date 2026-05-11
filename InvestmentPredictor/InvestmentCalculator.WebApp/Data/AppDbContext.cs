using Microsoft.EntityFrameworkCore;
using InvestmentCalculator.WebApp.Entities;
using System.Security.Cryptography.X509Certificates;

namespace InvestmentCalculator.WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MarketSnippet> MarketSnippets { get; set; }
    }
}

    