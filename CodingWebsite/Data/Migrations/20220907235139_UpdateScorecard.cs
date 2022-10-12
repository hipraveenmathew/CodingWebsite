using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWebsite.Data.Migrations
{
    public partial class UpdateScorecard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuesHead",
                table: "ScoreCard",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuesHead",
                table: "ScoreCard");
        }
    }
}
