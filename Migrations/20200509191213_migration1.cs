using Microsoft.EntityFrameworkCore.Migrations;

namespace SytePortfolio.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "main_img",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "main_img",
                table: "UserPortfolio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "main_img",
                table: "BlogUser",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "main_img",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "main_img",
                table: "UserPortfolio");

            migrationBuilder.DropColumn(
                name: "main_img",
                table: "BlogUser");
        }
    }
}
