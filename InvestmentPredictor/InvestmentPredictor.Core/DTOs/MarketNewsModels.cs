using System.Text.Json.Serialization;
namespace InvestmentPredictor.Core.DTOs
{
    public record FinnhubNewsItem
    {
        [JsonPropertyName("headline")] public string Headline { get; init; }
        [JsonPropertyName("summary")] public string Summary { get; init; }
        [JsonPropertyName("url")] public string Url { get; init; }
        [JsonPropertyName("source")] public string Source { get; init; }
    }
        

    public record MarketSummaryResult(string PolishSummary, string EnglishSummary);




}
