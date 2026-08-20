using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentCalculator.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddContentEnToMarketSnippet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentEn",
                table: "MarketSnippets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentEn",
                table: "MarketSnippets");
        }
    }
}
