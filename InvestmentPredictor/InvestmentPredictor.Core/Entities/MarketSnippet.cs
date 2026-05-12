namespace InvestmentPredictor.Core.Entities
{
    public class MarketSnippet
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string SummaryType { get; set; } = "Hourly";
    }
}
