using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWebsite.Data.Migrations
{
    public partial class updatedTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionHeading",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionHeading",
                table: "Questions");
        }
    }
}
