using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentCalculator.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSentimentScoreToMarketSnippet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SentimentScore",
                table: "MarketSnippets",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentimentScore",
                table: "MarketSnippets");
        }
    }
}
