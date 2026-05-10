using System.Text.Json.Serialization;

namespace InvestmentPredictor.Core.DTOs
{
    public class AlphaVantageNewsResponse
    {
        [JsonPropertyName("items")]
        public string Items { get; set; }

        [JsonPropertyName("feed")]
        public List<NewsArticles> Feed { get; set; }
    }

    public class NewsArticles
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("time_published")]
        public string TimePublished { get; set; }

        [JsonPropertyName("overall_sentiment_score")]
        public double SentimentScore { get; set; }
    }

}
