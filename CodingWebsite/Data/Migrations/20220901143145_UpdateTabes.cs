using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWebsite.Data.Migrations
{
    public partial class UpdateTabes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "versionIndex",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "language",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "versionIndex",
                table: "Answers");
        }
    }
}
